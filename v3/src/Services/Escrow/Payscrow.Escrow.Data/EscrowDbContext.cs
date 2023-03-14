using Microsoft.EntityFrameworkCore;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Domain.Models;

namespace Payscrow.Escrow.Data
{
    public class EscrowDbContext : DbContext, IEscrowDbContext
    {
        public EscrowDbContext(DbContextOptions<EscrowDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<AccountSetting> AccountSettings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<EscrowTransaction> EscrowTransactions { get; set; }
        public DbSet<EscrowTransactionAccount> EscrowTransactionAccounts { get; set; }
        public DbSet<Settlement> Settlements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EscrowDbContext).Assembly);

            modelBuilder.Seed();
        }
    }
}