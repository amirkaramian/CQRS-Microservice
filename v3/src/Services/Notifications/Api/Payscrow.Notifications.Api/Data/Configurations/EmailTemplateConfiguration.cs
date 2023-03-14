using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Notifications.Api.Application.Models;

namespace Payscrow.Notifications.Api.Data.Configurations
{
    public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.EmailProvider).WithMany(x => x.EmailTemplates).HasForeignKey(x => x.EmailProviderId);
        }
    }
}