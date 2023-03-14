using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Domain.Models
{
    public class PaymentMethodCurrency : Entity
    {
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}
