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
    public class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ChargeFixedRate)
             .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ChargePercentage)
             .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ChargeCap)
             .HasColumnType("decimal(18,2)");

            builder.Property(x => x.CustomerChargePercentage)
             .HasColumnType("decimal(18,2)");
        }
    }
}
