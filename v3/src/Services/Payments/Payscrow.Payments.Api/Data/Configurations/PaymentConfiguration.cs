using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Payments.Api.Domain.Models;

namespace Payscrow.Payments.Api.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.TransactionGuid);

            builder.Property(x => x.Amount)
             .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.PaymentMethod).WithMany(x => x.Payments).HasForeignKey(x => x.PaymentMethodId);
            builder.HasOne(x => x.Currency).WithMany().HasForeignKey(x => x.CurrencyId);
        }
    }
}
