using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.MarketPlace.Application.Domain.Entities;

namespace Payscrow.MarketPlace.Application.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Number).IsUnique(true);

            builder.Property(x => x.CustomerCharge).HasColumnType("decimal(18,2)");
            builder.Property(x => x.MerchantCharge).HasColumnType("decimal(18,2)");
            builder.Property(x => x.BrokerFee).HasColumnType("decimal(18,2)");
            builder.Property(x => x.GrandTotalPayable).HasColumnType("decimal(18,2)");

            builder.HasIndex(x => x.BrokerTransactionReference);
            builder.Property(x => x.BrokerTransactionReference).HasMaxLength(200);

            builder.HasOne(x => x.Currency).WithMany().HasForeignKey(x => x.CurrencyId);
        }
    }
}