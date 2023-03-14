using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Domain.Exceptions
{
    public class PaymentInviteDomainException : Exception
    {
        public PaymentInviteDomainException(string message) : base(message)
        {
        }

        public PaymentInviteDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public PaymentInviteDomainException()
        {
        }
    }
}
