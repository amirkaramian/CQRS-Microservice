using Payscrow.Payments.Api.Domain.Enumerations;
using System.Collections.Generic;

namespace Payscrow.Payments.Api.Domain.Models
{
    public class PaymentMethod : Entity
    {
        public PaymentMethod()
        {
            Payments = new HashSet<Payment>();
            PaymentMethodCurrencies = new HashSet<PaymentMethodCurrency>();
        }

        public PaymentMethodProvider Provider { get; set; }
        public string Name { get; set; }
        public string LogoFileName { get; set; }
        public string LogoUri { get; set; }
        public bool IsActive { get; set; }


        public ICollection<Payment> Payments { get; private set; }
        public ICollection<PaymentMethodCurrency> PaymentMethodCurrencies { get; private set; }
    }
}
