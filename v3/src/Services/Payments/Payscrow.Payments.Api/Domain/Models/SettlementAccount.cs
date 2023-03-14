using Payscrow.Payments.Api.Domain.Enumerations;
using System;

namespace Payscrow.Payments.Api.Domain.Models
{
    public class SettlementAccount : Entity
    {
        public decimal Amount { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string GatewayReference { get; set; }

        public SettlementStatus Status { get; set; }

        public Guid SettlementId { get; set; }
        public Settlement Settlement { get; set; }
    }
}