using Payscrow.Core.Events;
using System;

namespace Payscrow.Payments.Api.Application.IntegrationEvents.Publishing
{
    public class BulkSettlementToBankAccountsCompletedIntegrationEvent : IntegrationEvent
    {
        public BulkSettlementToBankAccountsCompletedIntegrationEvent(Guid tenantId, Guid transactionGuid) : base(tenantId)
        {
            TransactionGuid = transactionGuid;
        }

        public Guid TransactionGuid { get; }
    }
}