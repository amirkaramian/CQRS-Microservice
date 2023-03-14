using MediatR;
using System;

namespace Payscrow.Escrow.Application.Commands
{
    public class BaseCommand<T> : IRequest<T> where T : class
    {
        public Guid TenantId { get; set; }
    }
}