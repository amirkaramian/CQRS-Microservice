using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.PaymentInvite.Domain.Models;

namespace Payscrow.PaymentInvite.Data.Configurations
{
    public class TransactionStatusLogConfiguration : IEntityTypeConfiguration<TransactionStatusLog>
    {
        public void Configure(EntityTypeBuilder<TransactionStatusLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Transaction).WithMany().HasForeignKey(x => x.TransactionId);
        }
    }
}
