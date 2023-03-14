using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.EscrowDirect.Application.Domain.Entities;

namespace Payscrow.EscrowDirect.Data.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalChargeInclusive)
             .HasColumnType("decimal(18,2)");

            builder.Property(x => x.MerchantCharge)
             .HasColumnType("decimal(18,2)");

            builder.Property(x => x.CustomerCharge)
             .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Currency).WithMany().HasForeignKey(x => x.CurrencyId);
        }
    }
}
