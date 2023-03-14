using System;

namespace Payscrow.Payments.Api.Application.Common.Exceptions
{
    public class PaymentsDomainException : Exception
    {
        public PaymentsDomainException(string message) : base(message)
        {
        }

        public PaymentsDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public PaymentsDomainException()
        {
        }
    }
}