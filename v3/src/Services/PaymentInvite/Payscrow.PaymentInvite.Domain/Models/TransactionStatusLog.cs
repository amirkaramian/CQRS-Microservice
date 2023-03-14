using Payscrow.PaymentInvite.Domain.Enumerations;
using Payscrow.PaymentInvite.Domain.SeedWork;
using System;

namespace Payscrow.PaymentInvite.Domain.Models
{
    public class TransactionStatusLog : Entity
    {
        public int TransactionStatusId { get; set; }
        public TransactionStatus TransactionStatus => Enumeration.FromValue<TransactionStatus>(TransactionStatusId);

        public string Comment { get; set; }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
