using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Payscrow.Core.Bus;

namespace Payscrow.Infrastructure.RabbitMQBus
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIntegrationEventBus(this IServiceCollection services, IConfiguration configuration, string subscriptionClientName = null)
        {                     

            services.AddSingleton<IEventBus, RabbitMQEventBus>(sp => {
                var logger = sp.GetRequiredService<ILogger<RabbitMQEventBus>>();
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQEventBus(scopeFactory, configuration, logger, subscriptionClientName);
            });


            return services;
        }
    }
}
