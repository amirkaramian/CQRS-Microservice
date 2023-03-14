using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.MarketPlace.Application.Domain.Entities;

namespace Payscrow.MarketPlace.Application.Data.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.TenantId);

            builder.Property(x => x.ChargeCap).HasColumnType("decimal(18,2)");
        }
    }
}
