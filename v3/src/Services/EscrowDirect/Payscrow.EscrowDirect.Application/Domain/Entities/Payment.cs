using Payscrow.EscrowDirect.Application.Domain.Enumerations;
using System;

namespace Payscrow.EscrowDirect.Application.Domain.Entities
{
    public class Payment : AuditableEntity
    {
        public decimal Amount { get; set; }

        public PaymentType Type { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
