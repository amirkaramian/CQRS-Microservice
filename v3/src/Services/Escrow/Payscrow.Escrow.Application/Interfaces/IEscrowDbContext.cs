using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payscrow.Escrow.Domain.Models;

namespace Payscrow.Escrow.Application.Interfaces
{
    public interface IEscrowDbContext
    {
        DbSet<Tenant> Tenants { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<AccountSetting> AccountSettings { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<EscrowTransaction> EscrowTransactions { get; set; }
        DbSet<EscrowTransactionAccount> EscrowTransactionAccounts { get; set; }
        DbSet<Settlement> Settlements { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}