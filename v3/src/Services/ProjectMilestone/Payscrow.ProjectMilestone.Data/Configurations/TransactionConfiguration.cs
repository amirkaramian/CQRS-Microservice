using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.ProjectMilestone.Application.Domain.Models;

namespace Payscrow.ProjectMilestone.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
             .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId);
        }
    }
}
