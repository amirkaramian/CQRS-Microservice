using Microsoft.EntityFrameworkCore;
using Payscrow.Payments.Api.Domain.Models;

namespace Payscrow.Payments.Api.Data
{
    public class PaymentsDbContext : DbContext
    {
        public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : base(options)
        {
        }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<PaymentMethodCurrency> PaymentMethodCurrencies { get; set; }
        public DbSet<AccountPaymentMethod> AccountPaymentMethods { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<SettlementAccount> SettlementAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentsDbContext).Assembly);

            modelBuilder.Seed();
        }
    }
}