using System;

namespace Payscrow.Payments.Api.Domain.Models
{
    public class AccountPaymentMethod : Entity
    {
        public bool IsActive { get; set; }
        public Guid AccountId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
