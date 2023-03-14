using MicroRabbit.Domain.Core.Bus;
using Payscrow.Notifications.Api.Application.Enumerations;
using Payscrow.Notifications.Api.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Application.IntegrationEvents.Handlers.DealCreated
{
    public class SendDealCreationCompletionEmail : IEventHandler<DealCreatedEvent>, IEventHandlerService
    {
        private readonly IEmailNotificationService _emailNotificationService;

        public SendDealCreationCompletionEmail(IEmailNotificationService emailNotificationService)
        {
            _emailNotificationService = emailNotificationService;
        }

        public async Task Handle(DealCreatedEvent message)
        {
            var data = new Dictionary<string, object> { { "buyerLink", message.BuyerLink } };

            await _emailNotificationService.SendAsync(
                message.TenantId,
                EmailMessageType.DealCreatedAndVerified,
                message.SellerEmail,
                "Deal Creation Completion",
                data);
        }
    }
}