using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.PaymentInvite.Domain.Models;

namespace Payscrow.PaymentInvite.Data.Configurations
{
    public class TraderConfiguration : IEntityTypeConfiguration<Trader>
    {
        public void Configure(EntityTypeBuilder<Trader> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.AccountId).IsUnique();

            builder.OwnsOne(x => x.PhoneNumber);
            builder.OwnsOne(x => x.ContactAddress);
        }
    }
}
