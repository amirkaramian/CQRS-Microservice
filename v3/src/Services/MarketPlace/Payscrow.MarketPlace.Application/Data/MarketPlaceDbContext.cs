using Microsoft.EntityFrameworkCore;
using Payscrow.MarketPlace.Application.Domain.Entities;
using Payscrow.MarketPlace.Application.Interfaces;

namespace Payscrow.MarketPlace.Application.Data
{
    public class MarketPlaceDbContext : DbContext, IMarketPlaceDbContext
    {
        public MarketPlaceDbContext(DbContextOptions<MarketPlaceDbContext> options) : base(options)
        {
        }

        public DbSet<BrokerConfig> BrokerConfigs { get; set; }
        public DbSet<ChargeConfig> ChargeConfigs { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<SettlementAccount> SettlementAccounts { get; set; }
        public DbSet<TransactionStatusLog> TransactionStatusLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarketPlaceDbContext).Assembly);

            modelBuilder.Seed();
        }
    }
}