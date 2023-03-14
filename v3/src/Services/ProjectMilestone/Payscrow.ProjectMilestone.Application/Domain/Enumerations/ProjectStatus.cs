using Payscrow.ProjectMilestone.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Payscrow.ProjectMilestone.Application.Domain.Enumerations
{
    public class ProjectStatus : Enumeration
    {

        public static ProjectStatus Pending = new ProjectStatus(1, nameof(Pending).ToLowerInvariant(), "Pending");
        public static ProjectStatus InProgress = new ProjectStatus(2, nameof(InProgress).ToLowerInvariant(), "In Progress");
        public static ProjectStatus Completed = new ProjectStatus(3, nameof(Completed).ToLowerInvariant(), "Completed");


        protected ProjectStatus(int id, string name, string displayName) : base(id, name)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }


        public static IEnumerable<ProjectStatus> List() =>
            new[] { Pending, InProgress, Completed };


        public static ProjectStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new MilestoneDomainException($"Possible values for Milestone Status: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static ProjectStatus From(int id)
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
