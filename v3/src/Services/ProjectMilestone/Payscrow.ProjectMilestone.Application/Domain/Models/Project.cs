using Payscrow.ProjectMilestone.Application.Domain.Enumerations;
using System;
using System.Collections.Generic;

namespace Payscrow.ProjectMilestone.Application.Domain.Models
{
    public class Project : BaseModel
    {
        public string Title { get; set; }

        public string PayerAccountName { get; set; }
        public string PayeeAccountName { get; set; }

        public decimal? TotalAmount { get; set; }

        public Guid? PayerAccountId { get; set; }
        public Guid? PayeeAccountId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int StatusId { get; set; }
        public ProjectStatus Status => Enumeration.FromValue<ProjectStatus>(StatusId);

        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public ICollection<Milestone> Milestones { get; set; }
    }
}
