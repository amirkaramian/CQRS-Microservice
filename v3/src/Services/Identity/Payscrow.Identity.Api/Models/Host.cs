using System;

namespace Payscrow.Identity.Api.Models
{
    public class Host
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public string Name { get; set; }
    }
}
