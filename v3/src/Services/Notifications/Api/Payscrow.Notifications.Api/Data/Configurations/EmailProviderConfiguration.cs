using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Notifications.Api.Application.Models;

namespace Payscrow.Notifications.Api.Data.Configurations
{
    public class EmailProviderConfiguration : IEntityTypeConfiguration<EmailProvider>
    {
        public void Configure(EntityTypeBuilder<EmailProvider> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}