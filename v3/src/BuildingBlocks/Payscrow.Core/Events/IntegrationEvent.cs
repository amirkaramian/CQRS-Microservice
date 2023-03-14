using System;

namespace Payscrow.Core.Events
{
    public abstract class IntegrationEvent
    {
        public IntegrationEvent(Guid tenantId)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            TenantId = tenantId;
        }

        //[JsonProperty]
        public Guid Id { get; private set; }

        public Guid TenantId { get; private set; }

        //[JsonProperty]
        public DateTime CreationDate { get; private set; }
    }
}