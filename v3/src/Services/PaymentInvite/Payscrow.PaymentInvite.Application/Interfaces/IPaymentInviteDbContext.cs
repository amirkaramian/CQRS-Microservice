using Microsoft.EntityFrameworkCore;
using Payscrow.PaymentInvite.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.Interfaces
{
    public interface IPaymentInviteDbContext
    {
        DbSet<Trader> Traders {get; set;}
        DbSet<Deal> Deals { get; set; }
        DbSet<DealItem> DealItems { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<TransactionItem> TransactionItems { get; set; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<TransactionStatusLog> TransactionStatusLogs { get; set; }


        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
