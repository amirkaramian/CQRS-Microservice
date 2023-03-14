using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.ProjectMilestone.Application.Domain.Models;

namespace Payscrow.ProjectMilestone.Data.Configurations
{
    public class MilestoneConfiguration : IEntityTypeConfiguration<Milestone>
    {
        public void Configure(EntityTypeBuilder<Milestone> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
              .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Charge)
              .HasColumnType("decimal(18,2)");

            builder.Ignore(x => x.Status);

            builder.HasOne(x => x.Project).WithMany(x => x.Milestones).HasForeignKey(x => x.ProjectId);
        }
    }
}
