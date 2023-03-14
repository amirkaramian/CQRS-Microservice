using Payscrow.ProjectMilestone.Application.Domain.Enumerations;
using Payscrow.ProjectMilestone.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.ProjectMilestone.Application.BackgroundJobs
{
    public class MilestoneDueDateCheckerBackgroundJob
    {
        private readonly IMilestoneDbContext _context;

        public MilestoneDueDateCheckerBackgroundJob(IMilestoneDbContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            var now = DateTime.UtcNow;

            now = now.AddDays(-2);

            var dueMilestones = _context.Milestones.Where(x => x.Status == MilestoneStatus.InProgress
            && x.EndDate <= now).ToList();

            foreach (var milestone in dueMilestones)
            {

            }
        }
    }
}
