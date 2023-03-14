﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace Payscrow.PaymentInvite.Data
{
    /// <summary>
    /// <para>This allows us to run dotnet ef migrations commands without having to run ASP.NET Core's Startup.Configure()
    /// to set up context in the DI service. Unlike AccountContext, we don't really need to do this since there is
    /// one and only one CommonContext database, but for consistency, it's here.</para>
    /// <para>The syntax to create a migration is &quot;dotnet ef migrations add [name] -c CommonContext&quot;</para>
    /// <para>More info: http://benjii.me/2016/05/dotnet-ef-migrations-for-asp-net-core/ </para>
    /// </summary>
    public class PaymentInviteDbContextFactory : IDesignTimeDbContextFactory<PaymentInviteDbContext>
    {
        public PaymentInviteDbContext CreateDbContext(string[] args)
        {
            var migrationsAssembly = typeof(PaymentInviteDbContext).GetTypeInfo().Assembly.GetName().Name;

            var optionsBuilder = new DbContextOptionsBuilder<PaymentInviteDbContext>();
            optionsBuilder.UseSqlServer("Server=sqldata;Database=payscrow.identity.db;User Id=sa;Password=Pass@word;",
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(migrationsAssembly);
                        });

            return new PaymentInviteDbContext(optionsBuilder.Options);
        }
    }
}
