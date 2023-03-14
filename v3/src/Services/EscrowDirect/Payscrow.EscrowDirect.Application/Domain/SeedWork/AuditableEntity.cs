using System;

namespace Payscrow.EscrowDirect.Application.Domain
{
    public abstract class AuditableEntity : Entity
    {

        public Guid CreateUserId { get; set; }
        public DateTime CreateUtc { get; set; }

        public Guid? UpdateUserId { get; set; }
        public DateTime? UpdateUtc { get; set; }

        public AuditableEntity()
        {
            var utcNow = DateTime.UtcNow;
            CreateUtc = utcNow;
            UpdateUtc = utcNow;
        }
    }
}
