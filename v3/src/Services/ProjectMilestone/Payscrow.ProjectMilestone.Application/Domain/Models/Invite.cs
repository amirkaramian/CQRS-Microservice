using Payscrow.ProjectMilestone.Application.Domain.Enumerations;
using System;

namespace Payscrow.ProjectMilestone.Application.Domain.Models
{
    public class Invite : BaseModel
    {
        public string PartnerEmailAddress { get; set; }
        public ProjectPartnerRoleType RoleType { get; set; }
        public ProjectInviteStatus Status { get; set; }
        public string ResponseLink { get; set; }
        public int SentEmailCount { get; set; }


        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
