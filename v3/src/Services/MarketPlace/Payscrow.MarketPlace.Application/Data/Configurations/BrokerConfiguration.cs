using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.MarketPlace.Application.Domain.Entities;

namespace Payscrow.MarketPlace.Application.Data.Configurations
{
    public class BrokerConfiguration : IEntityTypeConfiguration<BrokerConfig>
    {  
        public void Configure(EntityTypeBuilder<BrokerConfig> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ChargeCap).HasColumnType("decimal(18,2)");
            builder.Property(x => x.FixedRate).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Percentage).HasColumnType("decimal(18,2)");
        }
    }
}
