using Payscrow.ProjectMilestone.Application.Domain.Enumerations;
using System;
using System.Collections.Generic;

namespace Payscrow.ProjectMilestone.Application.Domain.Models
{
    public class Milestone : BaseModel
    {
        public string Summary { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DueDate { get; set; }

        public decimal Amount { get; set; }
        public decimal Charge { get; set; }

        public bool InEscrow { get; set; }

        public int StatusId { get; set; }
        public MilestoneStatus Status => Enumeration.FromValue<MilestoneStatus>(StatusId);

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public ICollection<Deliverable> Deliverables { get; set; }
        
    }
}
