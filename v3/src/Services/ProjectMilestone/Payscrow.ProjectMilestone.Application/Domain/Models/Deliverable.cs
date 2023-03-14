using System;

namespace Payscrow.ProjectMilestone.Application.Domain.Models
{
    public class Deliverable : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid MilestoneId { get; set; }
        public Milestone Milestone { get; set; }
    }
}
