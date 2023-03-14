using Payscrow.EscrowDirect.Application.Domain.Enumerations;
using System;

namespace Payscrow.EscrowDirect.Application.Domain.Entities
{
    public class PaymentAttemptLog : AuditableEntity
    {
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public PaymentAttemptStatus Status { get; set; }



        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
