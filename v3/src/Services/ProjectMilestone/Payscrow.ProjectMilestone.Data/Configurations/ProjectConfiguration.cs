using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.ProjectMilestone.Application.Domain.Models;

namespace Payscrow.ProjectMilestone.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.TenantId);

            builder.HasOne(x => x.Currency).WithMany().HasForeignKey(x => x.CurrencyId);

            builder.Property(x => x.TotalAmount)
            .HasColumnType("decimal(18,2)");

            builder.Ignore(x => x.Status);
        }
    }
}
