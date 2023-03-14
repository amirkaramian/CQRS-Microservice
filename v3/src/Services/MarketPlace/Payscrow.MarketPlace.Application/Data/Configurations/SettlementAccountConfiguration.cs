using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.MarketPlace.Application.Domain.Entities;

namespace Payscrow.MarketPlace.Application.Data.Configurations
{
    public class SettlementAccountConfiguration : IEntityTypeConfiguration<SettlementAccount>
    {
        public void Configure(EntityTypeBuilder<SettlementAccount> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.AccountNumber).HasMaxLength(15);
            builder.Property(t => t.AccountName).HasMaxLength(100);
            builder.Property(t => t.BankCode).HasMaxLength(6);

            builder.HasOne(x => x.Transaction).WithMany(x => x.SettlementAccounts).HasForeignKey(x => x.TransactionId);
        }
    }
}
