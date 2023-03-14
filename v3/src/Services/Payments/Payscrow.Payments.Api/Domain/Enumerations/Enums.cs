using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Domain.Enumerations
{
    public enum PaymentMethodProvider
    {
        Flutterwave = 1,
        Paystack,
        Interswitch
    }

    public enum SettlementStatus
    {
        Pending,
        Initiated,
        Completed
    }
}