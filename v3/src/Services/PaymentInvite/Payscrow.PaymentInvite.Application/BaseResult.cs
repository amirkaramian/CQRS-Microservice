using System.Collections.Generic;

namespace Payscrow.PaymentInvite.Application
{
    public abstract class BaseResult
    {
        public bool Success => Errors.Count == 0;
        public List<(string, string)> Errors { get; set; } = new List<(string, string)>();
    }
}
