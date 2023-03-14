using System;

namespace Payscrow.Notifications.Api.Application.Commands
{
    public abstract class BaseCommand
    {
        public Guid TenantId { get; set; }
    }
}
