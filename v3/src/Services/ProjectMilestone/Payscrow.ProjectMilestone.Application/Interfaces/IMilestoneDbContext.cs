using Microsoft.EntityFrameworkCore;
using Payscrow.ProjectMilestone.Application.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.ProjectMilestone.Application.Interfaces
{
    public interface IMilestoneDbContext
    {
        DbSet<Project> Projects { get; set; }
        DbSet<Milestone> Milestones { get; set; }
        DbSet<Deliverable> Deliverables { get; set; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<PaymentLog> PaymentLogs { get; set; }
        DbSet<Invite> Invites { get; set; }


        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
