using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Identity.Api.Models;

namespace Payscrow.Identity.Api.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ApiKey).HasMaxLength(50);

            builder.Property(x => x.StreetAddress).HasMaxLength(300);
            builder.Property(x => x.City).HasMaxLength(100);
            builder.Property(x => x.State).HasMaxLength(100);
        }
    }
}
