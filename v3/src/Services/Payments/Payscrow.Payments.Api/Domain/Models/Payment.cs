using System;

namespace Payscrow.Payments.Api.Domain.Models
{
    public class Payment : Entity
    {
        public decimal Amount { get; set; }
        public Guid TransactionGuid { get; set; }
        public bool IsPaid { get; set; }

        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public string ExternalTransactionRef { get; set; }

        public Guid? PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}
