using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.ProjectMilestone.Application.Domain.Models;

namespace Payscrow.ProjectMilestone.Data.Configurations
{
    public class DeliverableConfiguration : IEntityTypeConfiguration<Deliverable>
    {
        public void Configure(EntityTypeBuilder<Deliverable> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Milestone).WithMany(x => x.Deliverables).HasForeignKey(x => x.MilestoneId);
        }
    }
}
