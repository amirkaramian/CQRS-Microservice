using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.PaymentInvite.Domain.Models;

namespace Payscrow.PaymentInvite.Data.Configurations
{
    public class DealConfiguration : IEntityTypeConfiguration<Deal>
    {
        public void Configure(EntityTypeBuilder<Deal> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SellerChargePercentage)
             .HasColumnType("decimal(18,2)");

            builder.OwnsOne(x => x.SellerPhone);

            builder.HasOne(x => x.Seller).WithMany(x => x.Deals).HasForeignKey(x => x.SellerId);
            builder.HasOne(x => x.Currency).WithMany().HasForeignKey(x => x.CurrencyId).IsRequired(true);
        }
    }
}
