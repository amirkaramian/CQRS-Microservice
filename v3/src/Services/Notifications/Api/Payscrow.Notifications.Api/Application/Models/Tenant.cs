using System;
using System.Collections.Generic;

namespace Payscrow.Notifications.Api.Application.Models
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }

        public ICollection<EmailTemplate> EmailTemplateReferences { get; set; }
    }
}