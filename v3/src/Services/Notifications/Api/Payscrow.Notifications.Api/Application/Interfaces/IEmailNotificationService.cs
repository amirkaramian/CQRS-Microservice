using Payscrow.Notifications.Api.Application.Enumerations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Application.Interfaces
{
    public interface IEmailNotificationService
    {
        Task SendAsync(Guid tenantId, EmailMessageType emailMessageType, string to, string subject, Dictionary<string, object> values);

        Task SendAsync(Guid tenantId, string to, string subject, string message);

        Task<string> GetEmailContent(Guid tenantId, EmailMessageType emailMessageType, Dictionary<string, object> values);
    }
}