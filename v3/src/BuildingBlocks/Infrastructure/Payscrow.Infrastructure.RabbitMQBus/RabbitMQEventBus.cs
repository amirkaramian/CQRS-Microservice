using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Payscrow.Core.Bus;
using Payscrow.Core.Events;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Payscrow.Infrastructure.RabbitMQBus
{
    public sealed class RabbitMQEventBus : IEventBus
    {
        private const string BROKER_NAME = "PAYSCROW_EVENT_BUS";

        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly string _queueName;

        private readonly int _retryCount;

        public RabbitMQEventBus(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration, ILogger<RabbitMQEventBus> logger, string queueName = null, int retryCount = 5)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
            _configuration = configuration;
            _logger = logger;
            _queueName = queueName;
            _retryCount = retryCount;
        }

        public void Publish<T>(T @event) where T : IntegrationEvent
        {
            var policy = Policy.Handle<BrokerUnreachableException>()
              .Or<SocketException>()
              .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
              {
                  _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
              });

            var factory = new ConnectionFactory() { HostName = _configuration["EventBusConnection"] };

            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME, type: ExchangeType.Direct);

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2; // persistent

                var eventName = @event.GetType().Name;

                _logger.LogInformation("Publishing event to RabbitMQ: {EventName}/{EventId}", eventName, @event.Id);

                channel.BasicPublish(exchange: BROKER_NAME, routingKey: eventName, basicProperties: properties, body: body);
            });
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(x => x.GetType() == handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{eventName}'");
            }

            _handlers[eventName].Add(handlerType);

            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : IntegrationEvent
        {
            var factory = new ConnectionFactory() { HostName = _configuration["EventBusConnection"], DispatchConsumersAsync = true };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME, type: ExchangeType.Direct);

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var eventName = typeof(T).Name;

            channel.QueueBind(queue: _queueName, exchange: BROKER_NAME, routingKey: eventName);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;

            _logger.LogInformation("Started basic consume of Event: {eventName}", eventName);

            channel.BasicConsume(_queueName, true, consumer);
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing event");
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    foreach (var subscription in _handlers[eventName])
                    {
                        var handler = scope.ServiceProvider.GetService(subscription);
                        if (handler == null)
                        {
                            _logger.LogWarning("---- No handlers found to handle the event: {EventName}|{Subscription} -----", eventName, subscription.FullName);
                            continue;
                        }
                        var eventType = _eventTypes.SingleOrDefault(x => x.Name == eventName);

                        var @event = JsonConvert.DeserializeObject(message, eventType);
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                    }
                }
            }
        }
    }
}