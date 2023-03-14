using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.ProjectMilestone.Application.Domain.Models;

namespace Payscrow.ProjectMilestone.Data.Configurations
{
    public class InviteConfiguration : IEntityTypeConfiguration<Invite>
    {
        public void Configure(EntityTypeBuilder<Invite> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId);
        }
    }
}
