using System;

namespace Payscrow.EscrowDirect.Application.Common.Exceptions
{
    public class MilestoneDomainException : Exception
    {
        public MilestoneDomainException(string message) : base(message)
        {
        }

        public MilestoneDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MilestoneDomainException()
        {
        }
    }
}
