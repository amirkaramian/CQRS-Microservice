using Payscrow.Core.Events;
using System;

namespace Payscrow.ProjectMilestone.Application.IntegrationEvents
{
    public class MilestoneDueIntegrationEvent : IntegrationEvent
    {
        public MilestoneDueIntegrationEvent(string implementerName, Guid tenantId) : base(tenantId)
        {
            ImplementerName = implementerName;
        }

        public string ImplementerName { get; }
    }
}