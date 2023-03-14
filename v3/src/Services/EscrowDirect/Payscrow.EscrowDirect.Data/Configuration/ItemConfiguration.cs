using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.EscrowDirect.Application.Domain.Entities;

namespace Payscrow.EscrowDirect.Data.Configuration
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price)
             .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Description).HasMaxLength(2000);
            builder.Property(x => x.Name).HasMaxLength(300);

            builder.HasOne(x => x.Transaction).WithMany(x => x.Items).HasForeignKey(x => x.TransactionId);
        }
    }
}
