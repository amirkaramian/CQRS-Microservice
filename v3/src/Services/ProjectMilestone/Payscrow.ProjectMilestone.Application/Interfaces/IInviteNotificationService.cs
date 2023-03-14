using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.ProjectMilestone.Application.Interfaces
{
    public interface IInviteNotificationService
    {
        Task SendAsync(InviteNotificationRequest request);
    }

    public class InviteNotificationRequest
    {
        public Guid TenantId { get; set; }
        public Guid InviteId { get; set; }
        public string AccountName { get; set; }
    }
}
