using Microsoft.EntityFrameworkCore;
using Payscrow.Notifications.Api.Application.Models;
using Payscrow.Notifications.Api.Interfaces;

namespace Payscrow.Notifications.Api.Data
{
    public class NotificationDbContext : DbContext, INotificationDbContext
    {
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<EmailProvider> EmailProviders { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
            modelBuilder.Seed();
        }
    }
}