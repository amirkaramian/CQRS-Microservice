using MediatR;
using System;

namespace Payscrow.MarketPlace.Application.Commands
{
    public abstract class BaseCommand<T> : IRequest<T> where T : BaseCommandResult
    {
        public Guid TenantId { get; set; }
    }
}
