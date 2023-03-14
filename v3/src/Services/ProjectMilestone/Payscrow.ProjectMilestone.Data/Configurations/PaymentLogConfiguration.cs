using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.ProjectMilestone.Application.Domain.Models;

namespace Payscrow.ProjectMilestone.Data.Configurations
{
    public class PaymentLogConfiguration : IEntityTypeConfiguration<PaymentLog>
    {
        public void Configure(EntityTypeBuilder<PaymentLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
             .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId);
        }
    }
}
