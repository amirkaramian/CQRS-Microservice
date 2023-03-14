using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.PaymentInvite.Domain.Models;

namespace Payscrow.PaymentInvite.Data.Configurations
{
    public class DealItemConfiguration : IEntityTypeConfiguration<DealItem>
    {
        public void Configure(EntityTypeBuilder<DealItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
              .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Deal).WithMany(x => x.Items).HasForeignKey(x => x.DealId);
        }
    }
}
