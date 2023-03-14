using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.EscrowDirect.Application.Domain.Entities;

namespace Payscrow.EscrowDirect.Data.Configuration
{
    public class PaymentAttemptLogConfiguration : IEntityTypeConfiguration<PaymentAttemptLog>
    {
        public void Configure(EntityTypeBuilder<PaymentAttemptLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
            .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Transaction).WithMany().HasForeignKey(x => x.TransactionId);
        }
    }
}
