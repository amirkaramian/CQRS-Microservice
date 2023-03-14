using Payscrow.Core.Events;

namespace Payscrow.Escrow.Application.IntegrationEvents
{
    public class TenantCreatedEvent : IntegrationEvent
    {
        public string CompanyName { get; }
        public string ContactPhone { get; }
        public string ContactEmail { get; }
        public string DomainName { get; }

        public TenantCreatedEvent(string tenantId, string companyName, string contactPhone, string contactEmail, string domainName)
            : base(tenantId.ToGuid())
        {
            CompanyName = companyName;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            DomainName = domainName;
        }
    }
}