using MicroRabbit.Domain.Core.Bus;
using Payscrow.Core.Events;

namespace Payscrow.Core.Bus
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : IntegrationEvent;
        void Subscribe<T, TH>() where T : IntegrationEvent where TH : IEventHandler<T>;
    }
}
