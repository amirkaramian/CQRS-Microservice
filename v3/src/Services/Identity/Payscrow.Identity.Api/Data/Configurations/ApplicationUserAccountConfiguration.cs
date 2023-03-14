using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Identity.Api.Models;

namespace Payscrow.Identity.Api.Data.Configurations
{
    public class ApplicationUserAccountConfiguration : IEntityTypeConfiguration<ApplicationUserAccount>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserAccount> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
