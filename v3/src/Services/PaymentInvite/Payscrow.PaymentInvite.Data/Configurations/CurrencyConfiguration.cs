using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.PaymentInvite.Domain.Models;

namespace Payscrow.PaymentInvite.Data.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Code).IsUnique(true);

            builder.Property(x => x.FixedCharge)
             .HasColumnType("decimal(18,2)");

            builder.Property(x => x.PercentageCharge)
             .HasColumnType("decimal(18,2)");
        }
    }
}
