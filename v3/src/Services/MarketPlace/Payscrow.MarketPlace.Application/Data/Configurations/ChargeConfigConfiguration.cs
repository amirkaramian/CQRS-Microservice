using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.MarketPlace.Application.Domain.Entities;

namespace Payscrow.MarketPlace.Application.Data.Configurations
{
    public class ChargeConfigConfiguration : IEntityTypeConfiguration<ChargeConfig>
    {
        public void Configure(EntityTypeBuilder<ChargeConfig> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FixedRate).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Percentage).HasColumnType("decimal(18,2)");
            builder.Property(x => x.MaxTransactionAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.MinTransactionAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Currency).WithMany().HasForeignKey(x => x.CurrencyId);
        }
    }
}
