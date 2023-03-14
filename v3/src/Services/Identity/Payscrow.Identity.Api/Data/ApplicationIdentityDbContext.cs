using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Payscrow.Identity.Api.Models;
using System;

namespace Payscrow.Identity.Api.Data
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<ApplicationUserAccount> ApplicationUserAccounts { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Let IdentityDbContext do its work.
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationIdentityDbContext).Assembly);

            builder.Seed();
        }
    }
}