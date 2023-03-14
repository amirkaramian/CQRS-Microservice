using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Payments.Api.Domain.Models;

namespace Payscrow.Payments.Api.Data.Configurations
{
    public class SettlementConfiguration : IEntityTypeConfiguration<Settlement>
    {
        public void Configure(EntityTypeBuilder<Settlement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CurrencyCode).HasMaxLength(3);
            builder.HasIndex(x => x.TransactionGuid);
        }
    }
}