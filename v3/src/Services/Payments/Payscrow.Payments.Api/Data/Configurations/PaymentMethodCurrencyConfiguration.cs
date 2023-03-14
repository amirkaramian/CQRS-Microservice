using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Payments.Api.Domain.Models;

namespace Payscrow.Payments.Api.Data.Configurations
{
    public class PaymentMethodCurrencyConfiguration : IEntityTypeConfiguration<PaymentMethodCurrency>
    {
        public void Configure(EntityTypeBuilder<PaymentMethodCurrency> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Currency).WithMany(x => x.CurrencyPaymentMethods).HasForeignKey(x => x.CurrencyId);
            builder.HasOne(x => x.PaymentMethod).WithMany(x => x.PaymentMethodCurrencies).HasForeignKey(x => x.PaymentMethodId);
        }
    }
}
