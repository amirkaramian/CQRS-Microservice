using Payscrow.Core.Events;
using System;

namespace Payscrow.MarketPlace.Application.IntegrationEvents.Publishing
{
    public class MarketPlaceTransactionDisputedEvent : IntegrationEvent
    {
        public Guid TransactionGuid { get; }
        public Guid MerchantAccountGuid { get; }
        public Guid CustomerAccountGuid { get; }
        public Guid BrokerAccountGuid { get; }
        public Guid DisputeRaisedByAccountId { get; }
        public string Complaint { get; }

        public MarketPlaceTransactionDisputedEvent(Guid tenantId, Guid transactionGuid,
            Guid merchantAccountGuid,
            Guid customerAccountGuid,
            Guid brokerAccountGuid,
            Guid disputeRaisedByAccountId,
            string complaint) : base(tenantId)
        {
            TransactionGuid = transactionGuid;
            MerchantAccountGuid = merchantAccountGuid;
            CustomerAccountGuid = customerAccountGuid;
            BrokerAccountGuid = brokerAccountGuid;
            DisputeRaisedByAccountId = disputeRaisedByAccountId;
            Complaint = complaint;
        }
    }
}