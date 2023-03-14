using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Identity.Api.Models;

namespace Payscrow.Identity.Api.Data.Configurations
{
    public class HostConfiguration : IEntityTypeConfiguration<Host>
    {
        public void Configure(EntityTypeBuilder<Host> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Name).IsUnique(true);

            builder.HasOne(x => x.Tenant).WithMany().HasForeignKey(x => x.TenantId);
        }
    }
}
