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
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ChargeCap)
             .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Code).HasMaxLength(3).IsFixedLength(true);
            builder.HasIndex(x => x.Code);
        }
    }
}
