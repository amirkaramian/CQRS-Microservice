using Payscrow.Payments.Api.Domain.Enumerations;
using System;
using System.Collections.Generic;

namespace Payscrow.Payments.Api.Domain.Models
{
    public class Settlement : Entity
    {
        public Guid TransactionGuid { get; set; }
        public string CurrencyCode { get; set; }
        public SettlementStatus Status { get; set; }

        public PaymentMethodProvider Provider { get; set; }
        public string GatewayReference { get; set; }

        public ICollection<SettlementAccount> SettlementAccounts { get; set; }
    }
}