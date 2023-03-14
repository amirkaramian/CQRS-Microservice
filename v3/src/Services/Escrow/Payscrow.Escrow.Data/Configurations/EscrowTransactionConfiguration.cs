using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Escrow.Domain.Models;

namespace Payscrow.Escrow.Data.Configurations
{
    public class EscrowTransactionConfiguration : IEntityTypeConfiguration<EscrowTransaction>
    {
        public void Configure(EntityTypeBuilder<EscrowTransaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.EscrowCode).HasMaxLength(10);

            builder.Ignore(x => x.Status);
            builder.Ignore(x => x.ServiceType);

            builder.Property(x => x.Amount)
             .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Currency).WithMany().HasForeignKey(x => x.CurrencyId);
        }
    }
}