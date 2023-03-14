using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payscrow.PaymentInvite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Number);

            builder.OwnsOne(x => x.BuyerPhone);
            builder.OwnsOne(x => x.DeliveryAddress);
            builder.HasIndex(x => x.BuyerEmail);

            builder.Property(x => x.BuyerChargeAmount)
              .HasColumnType("decimal(18,2)");

            builder.Property(x => x.SellerChargeAmount)
              .HasColumnType("decimal(18,2)");

            builder.Property(x => x.TotalAmount)
              .HasColumnType("decimal(18,2)");

            builder.Ignore(x => x.Status);



            builder.HasOne(x => x.Deal).WithMany().HasForeignKey(x => x.DealId);

            builder.HasOne(x => x.Buyer).WithMany().HasForeignKey(x => x.BuyerId);

            builder.HasOne(x => x.Currency).WithMany().HasForeignKey(x => x.CurrencyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
