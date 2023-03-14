using Payscrow.Core.Events;
using System;
using System.Collections.Generic;

namespace Payscrow.MarketPlace.Application.IntegrationEvents
{
    public class MarketPlacePaymentVerifiedEvent : IntegrationEvent
    {
        public MarketPlacePaymentVerifiedEvent(Guid tenantId, Guid transactionGuid, string transactionNumber, string escrowCode, Guid customerAccountId, string customerEmailAddress,
            string customerPhone, Guid merchantAccountId, string merchantEmailAddress, string merchantPhone, Guid brokerAccountId, decimal amountPaid, string currencyCode,
            List<Settlement> settlements)
            : base(tenantId)
        {
            TransactionGuid = transactionGuid;
            EscrowCode = escrowCode;
            TransactionNumber = transactionNumber;
            CustomerAccountId = customerAccountId;
            CustomerEmailAddress = customerEmailAddress;
            CustomerPhone = customerPhone;
            MerchantAccountId = merchantAccountId;
            MerchantEmailAddress = merchantEmailAddress;
            MerchantPhone = merchantPhone;
            BrokerAccountId = brokerAccountId;
            AmountPaid = amountPaid;
            CurrencyCode = currencyCode;
            Settlements = settlements;
        }

        public Guid TransactionGuid { get; }
        public string TransactionNumber { get; }
        public string EscrowCode { get; }
        public Guid CustomerAccountId { get; }
        public string CustomerEmailAddress { get; }
        public string CustomerPhone { get; }
        public Guid MerchantAccountId { get; }
        public string MerchantEmailAddress { get; }
        public string MerchantPhone { get; }
        public Guid BrokerAccountId { get; }

        public decimal AmountPaid { get; }
        public string CurrencyCode { get; }

        public List<Settlement> Settlements { get; }

        public class Settlement
        {
            public string BankCode { get; set; }
            public string AccountNumber { get; set; }
            public string AccountName { get; set; }
            public decimal Amount { get; set; }
        }
    }
}