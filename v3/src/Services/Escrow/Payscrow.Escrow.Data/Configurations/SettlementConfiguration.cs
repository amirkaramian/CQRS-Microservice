using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Escrow.Domain.Models;

namespace Payscrow.Escrow.Data.Configurations
{
    public class SettlementConfiguration : IEntityTypeConfiguration<Settlement>
    {
        public void Configure(EntityTypeBuilder<Settlement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
             .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.EscrowTransaction).WithMany().HasForeignKey(x => x.EscrowTransactionId);
        }
    }
}