using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Identity.Api.Models;

namespace Payscrow.Identity.Api.Data.Configurations
{
    public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
    {
        public void Configure(EntityTypeBuilder<ApiKey> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Label).HasMaxLength(200);

            builder.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId);
        }
    }
}