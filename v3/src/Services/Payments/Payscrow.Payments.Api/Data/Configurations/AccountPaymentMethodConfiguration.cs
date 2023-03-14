using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Payments.Api.Domain.Models;

namespace Payscrow.Payments.Api.Data.Configurations
{
    public class AccountPaymentMethodConfiguration : IEntityTypeConfiguration<AccountPaymentMethod>
    {
        public void Configure(EntityTypeBuilder<AccountPaymentMethod> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(x => x.PaymentMethod).WithMany().HasForeignKey(x => x.PaymentMethodId);
        }
    }
}
