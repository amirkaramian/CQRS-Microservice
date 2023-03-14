using System;

namespace Payscrow.MarketPlace.Application.Domain
{
    public interface IAuditableEntity
    {
        Guid CreateUserId { get; set; }
        DateTime CreateUtc { get; }

        Guid? UpdateUserId { get; set; }
        DateTime? UpdateUtc { get; set; }
    }
}