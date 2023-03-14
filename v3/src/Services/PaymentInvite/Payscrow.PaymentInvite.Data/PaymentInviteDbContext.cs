using Microsoft.EntityFrameworkCore;
using Payscrow.PaymentInvite.Application.Interfaces;
using Payscrow.PaymentInvite.Domain.Models;

namespace Payscrow.PaymentInvite.Data
{
    public class PaymentInviteDbContext : DbContext, IPaymentInviteDbContext
    {
        public PaymentInviteDbContext(DbContextOptions<PaymentInviteDbContext> options) : base(options) { }

        public DbSet<Trader> Traders { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<DealItem> DealItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<TransactionStatusLog> TransactionStatusLogs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentInviteDbContext).Assembly);

            modelBuilder.Seed();
        }
    }
}
