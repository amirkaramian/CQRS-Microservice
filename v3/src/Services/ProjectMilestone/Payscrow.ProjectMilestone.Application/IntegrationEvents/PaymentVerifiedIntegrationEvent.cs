using Payscrow.Core.Events;
using System;

namespace Payscrow.ProjectMilestone.Application.IntegrationEvents
{
    public class PaymentVerifiedIntegrationEvent : IntegrationEvent
    {
        public PaymentVerifiedIntegrationEvent(string transactionId, Guid tenantId) : base(tenantId)
        {
            TransactionId = transactionId;
        }

        public string TransactionId { get; }
    }
}