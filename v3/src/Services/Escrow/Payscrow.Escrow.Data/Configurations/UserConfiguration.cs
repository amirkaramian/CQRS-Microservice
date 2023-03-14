using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.Escrow.Domain.Models;

namespace Payscrow.Escrow.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserGuid).IsUnique();
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
            builder.Property(x => x.MiddleName).HasMaxLength(100);
            builder.Property(x => x.PhoneNumber).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(150);
        }
    }
}