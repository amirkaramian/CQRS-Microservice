using Payscrow.Notifications.Api.Application.Enumerations;
using System.Collections.Generic;

namespace Payscrow.Notifications.Api.Application.Models
{
    public class EmailProvider : BaseModel
    {
        public EmailProviderType Type { get; set; }
        public string ProviderApiKey { get; set; }

        public ICollection<EmailTemplate> EmailTemplates { get; set; }
    }
}