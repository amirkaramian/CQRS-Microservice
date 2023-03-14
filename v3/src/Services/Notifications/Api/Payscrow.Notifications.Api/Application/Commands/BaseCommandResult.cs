using System.Collections.Generic;

namespace Payscrow.Notifications.Api.Application.Commands
{
    public abstract class BaseCommandResult
    {
        public bool IsSuccessful => Errors.Count == 0;
        public List<(string, string)> Errors { get; set; } = new List<(string, string)>();
    }
}
