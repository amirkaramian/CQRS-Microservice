using System.Collections.Generic;

namespace Payscrow.Payments.Api.Domain.Models
{
    public class Currency : Entity
    {
        public Currency()
        {
            CurrencyPaymentMethods = new HashSet<PaymentMethodCurrency>();
        }

        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<PaymentMethodCurrency> CurrencyPaymentMethods { get; private set; }
    }
}
