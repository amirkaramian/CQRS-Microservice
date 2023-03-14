using System;

namespace Payscrow.DisputeResolution.Application.Common.Exceptions
{
    public class DisputeResolutionDomainException : Exception
    {
        public DisputeResolutionDomainException(string message) : base(message)
        {
        }

        public DisputeResolutionDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DisputeResolutionDomainException()
        {
        }
    }
}
