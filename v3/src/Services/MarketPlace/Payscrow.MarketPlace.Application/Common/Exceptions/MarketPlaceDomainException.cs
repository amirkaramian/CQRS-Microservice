using System;

namespace Payscrow.MarketPlace.Application.Common.Exceptions
{
    public class MarketPlaceDomainException : Exception
    {
        public MarketPlaceDomainException(string message) : base(message)
        {
        }

        public MarketPlaceDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MarketPlaceDomainException()
        {
        }
    }
}
