using Microsoft.EntityFrameworkCore;
using Payscrow.DisputeResolution.Application.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.DisputeResolution.Application.Interfaces
{
    // Interface for DisputeDbContext
    public interface IDisputeDbContext
    {
        DbSet<DisputedTransaction> DisputedTransactions { get; set; }
        DbSet<DisputedTransactionChatRecord> DisputedTransactionChatRecords { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}