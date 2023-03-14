using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Escrow.Domain.Models;

namespace Payscrow.Escrow.Data.Configurations
{
    public class AccountSettingConfiguration : IEntityTypeConfiguration<AccountSetting>
    {
        public void Configure(EntityTypeBuilder<AccountSetting> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId);

            builder.HasOne(x => x.DefaultCurrency).WithMany().HasForeignKey(x => x.DefaultCurrencyId);
        }
    }
}