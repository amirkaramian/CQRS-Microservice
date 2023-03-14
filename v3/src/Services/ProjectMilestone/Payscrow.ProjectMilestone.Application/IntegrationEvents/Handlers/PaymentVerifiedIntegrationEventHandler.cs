using MicroRabbit.Domain.Core.Bus;
using Payscrow.ProjectMilestone.Application.Interfaces.Markers;
using System.Threading.Tasks;

namespace Payscrow.ProjectMilestone.Application.IntegrationEvents.Handlers
{
    public class PaymentVerifiedIntegrationEventHandler : IEventHandler<PaymentVerifiedIntegrationEvent>, IIntegrationEventHandlerScopedLifetime
    {
        public async Task Handle(PaymentVerifiedIntegrationEvent notification)
        {
            await Task.CompletedTask;
        }
    }
}