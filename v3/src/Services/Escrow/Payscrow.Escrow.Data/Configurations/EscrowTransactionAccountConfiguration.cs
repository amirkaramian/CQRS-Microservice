using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Escrow.Domain.Models;

namespace Payscrow.Escrow.Data.Configurations
{
    public class EscrowTransactionAccountConfiguration : IEntityTypeConfiguration<EscrowTransactionAccount>
    {
        public void Configure(EntityTypeBuilder<EscrowTransactionAccount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.EscrowTransaction).WithMany(x => x.EscrowTransactionAccounts).HasForeignKey(x => x.EscrowTransactionId);
            builder.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId);
        }
    }
}