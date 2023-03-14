using Microsoft.EntityFrameworkCore;
using Payscrow.Notifications.Api.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Interfaces
{
    public interface INotificationDbContext
    {
        DbSet<EmailLog> EmailLogs { get; set; }
        DbSet<Tenant> Tenants { get; set; }
        DbSet<EmailProvider> EmailProviders { get; set; }
        DbSet<EmailTemplate> EmailTemplates { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}