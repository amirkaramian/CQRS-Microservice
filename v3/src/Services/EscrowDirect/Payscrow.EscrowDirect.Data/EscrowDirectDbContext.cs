using Microsoft.EntityFrameworkCore;
using Payscrow.EscrowDirect.Application.Domain.Entities;
using Payscrow.EscrowDirect.Application.Interfaces;

namespace Payscrow.EscrowDirect.Data
{
    public class EscrowDirectDbContext : DbContext, IEscrowDirectDbContext
    {
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentAttemptLog> PaymentAttemptLogs { get; set; }
        public DbSet<ChargeConfig> ChargeConfigurations { get; set; }





        public EscrowDirectDbContext(DbContextOptions<EscrowDirectDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EscrowDirectDbContext).Assembly);

            modelBuilder.Seed();
        }
    }
}
