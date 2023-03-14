using System;

namespace Payscrow.PaymentInvite.Application.Commands
{
    public class BaseCommand
    {
        public Guid TenantId { get; set; }
    }
}