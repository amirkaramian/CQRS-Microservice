using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.MarketPlace.Application.Domain.Entities;

namespace Payscrow.MarketPlace.Application.Data.Configurations
{
    public class TransactionStatusLogConfiguration : IEntityTypeConfiguration<TransactionStatusLog>
    {
        public void Configure(EntityTypeBuilder<TransactionStatusLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Transaction).WithMany(x => x.TransactionStatusLogs).HasForeignKey(x => x.TransactionId);
        }
    }
}