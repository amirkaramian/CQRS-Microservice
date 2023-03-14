using System;

namespace Payscrow.MarketPlace.Application.Domain.Entities
{
    public class SettlementAccount : Entity
    {
        public string BankCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
