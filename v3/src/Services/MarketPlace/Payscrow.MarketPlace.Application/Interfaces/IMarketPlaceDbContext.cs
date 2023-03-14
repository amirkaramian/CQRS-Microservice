using Microsoft.EntityFrameworkCore;
using Payscrow.MarketPlace.Application.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.Interfaces
{
    public interface IMarketPlaceDbContext
    {
        DbSet<BrokerConfig> BrokerConfigs { get; set; }
        DbSet<ChargeConfig> ChargeConfigs { get; set; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<SettlementAccount> SettlementAccounts { get; set; }
        DbSet<TransactionStatusLog> TransactionStatusLogs { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}