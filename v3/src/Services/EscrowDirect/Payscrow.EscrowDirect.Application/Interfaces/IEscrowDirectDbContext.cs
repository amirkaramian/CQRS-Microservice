using Microsoft.EntityFrameworkCore;
using Payscrow.EscrowDirect.Application.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.EscrowDirect.Application.Interfaces
{
    public interface IEscrowDirectDbContext
    {
        DbSet<Merchant> Merchants { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<PaymentAttemptLog> PaymentAttemptLogs { get; set; }
        DbSet<ChargeConfig> ChargeConfigurations { get; set; }


        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
