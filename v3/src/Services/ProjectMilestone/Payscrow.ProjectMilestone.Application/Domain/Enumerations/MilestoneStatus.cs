using Payscrow.ProjectMilestone.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Payscrow.ProjectMilestone.Application.Domain.Enumerations
{
    public class MilestoneStatus : Enumeration
    {

        public static MilestoneStatus Pending = new MilestoneStatus(1, nameof(Pending).ToLowerInvariant(), "Pending");
        public static MilestoneStatus InProgress = new MilestoneStatus(2, nameof(InProgress).ToLowerInvariant(), "In Progress");
        public static MilestoneStatus Completed = new MilestoneStatus(3, nameof(Completed).ToLowerInvariant(), "Completed");


        protected MilestoneStatus(int id, string name, string displayName) : base(id, name)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }


        public static IEnumerable<MilestoneStatus> List() =>
            new[] { Pending, InProgress, Completed };


        public static MilestoneStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new MilestoneDomainException($"Possible values for Milestone Status: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static MilestoneStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new MilestoneDomainException($"Possible values for Milestone Status: {string.Join(",", List().Select(s => s.Id))}");
            }

            return state;
        }
    }
}
