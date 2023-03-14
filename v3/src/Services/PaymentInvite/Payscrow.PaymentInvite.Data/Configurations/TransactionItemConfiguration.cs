using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.PaymentInvite.Domain.Models;

namespace Payscrow.PaymentInvite.Data.Configurations
{
    public class TransactionItemConfiguration : IEntityTypeConfiguration<TransactionItem>
    {
        public void Configure(EntityTypeBuilder<TransactionItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
             .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Transaction).WithMany(x => x.Items).HasForeignKey(x => x.TransactionId);
        }
    }
}
