using Microsoft.EntityFrameworkCore;
using Payscrow.ProjectMilestone.Application.Domain.Models;
using Payscrow.ProjectMilestone.Application.Interfaces;

namespace Payscrow.ProjectMilestone.Data
{
    public class MilestoneDbContext : DbContext, IMilestoneDbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Deliverable> Deliverables { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PaymentLog> PaymentLogs { get; set; }
        public DbSet<Invite> Invites { get; set; }

        public MilestoneDbContext(DbContextOptions<MilestoneDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MilestoneDbContext).Assembly);

            modelBuilder.Seed();
        }
    }
}
