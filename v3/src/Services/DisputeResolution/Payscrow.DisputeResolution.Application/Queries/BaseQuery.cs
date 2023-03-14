using MediatR;
using System;

namespace Payscrow.DisputeResolution.Application.Queries
{
    public class BaseQuery<T> : IRequest<T> where T : class
    {
        public Guid TenantId { get; set; }
    }
}
