using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Payments.Api.Domain.Models;

namespace Payscrow.Payments.Api.Data.Configurations
{
    public class SettlementAccountConfiguration : IEntityTypeConfiguration<SettlementAccount>
    {
        public void Configure(EntityTypeBuilder<SettlementAccount> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
             .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Settlement).WithMany(x => x.SettlementAccounts).HasForeignKey(x => x.SettlementId);
        }
    }
}