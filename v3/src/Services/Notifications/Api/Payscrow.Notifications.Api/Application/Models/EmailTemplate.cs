using Payscrow.Notifications.Api.Application.Enumerations;
using System;

namespace Payscrow.Notifications.Api.Application.Models
{
    public class EmailTemplate : BaseModel
    {
        public EmailMessageType MessageType { get; set; }

        public string ProviderTemplateId { get; set; }
        public string Content { get; set; }

        public Guid EmailProviderId { get; set; }
        public EmailProvider EmailProvider { get; set; }
    }
}