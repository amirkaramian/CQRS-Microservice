using Payscrow.Core.Events;
using System.Threading.Tasks;

namespace MicroRabbit.Domain.Core.Bus
{
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : IntegrationEvent
    {
        Task Handle(TEvent @event);
       
    }

    public interface IEventHandler
    {

    }
}
