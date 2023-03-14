using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.EscrowDirect.Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.EscrowDirect.Data.Configuration
{
    public class ChargeConfigConfiguration : IEntityTypeConfiguration<ChargeConfig>
    {
        public void Configure(EntityTypeBuilder<ChargeConfig> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.MaxTransactionAmount);
            builder.HasIndex(x => x.MinTransactionAmount);

            builder.HasOne(x => x.Currency).WithMany(x => x.ChargeConfigurations).HasForeignKey(x => x.CurrencyId);

            builder.Property(x => x.Percentage)
             .HasColumnType("decimal(18,2)");

            builder.Property(x => x.FixedRate)
             .HasColumnType("decimal(18,2)");

            builder.Property(x => x.MaxTransactionAmount)
             .HasColumnType("decimal(18,2)");

            builder.Property(x => x.MinTransactionAmount)
             .HasColumnType("decimal(18,2)");
        }
    }
}
