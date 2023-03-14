using Payscrow.MarketPlace.Application.Domain.Enumerations;
using System;

namespace Payscrow.MarketPlace.Application.Domain.Entities
{
    public class Payment : Entity
    {
        public decimal Amount { get; set; }
        public PaymentType Type { get; set; }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }

        public DateTime? PaymentDate { get; set; }
    }
}