using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.MarketPlace.Application.Domain.Entities;

namespace Payscrow.MarketPlace.Application.Data.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(300);
            builder.Property(x => x.Description).HasMaxLength(4000);

            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Transaction).WithMany(x => x.Items).HasForeignKey(x => x.TransactionId);
        }
    }
}
