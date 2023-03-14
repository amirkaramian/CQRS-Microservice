﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace Payscrow.MarketPlace.Application.Data
{
    /// <summary>
    /// <para>This allows us to run dotnet ef migrations commands without having to run ASP.NET Core's Startup.Configure()
    /// to set up context in the DI service. Unlike AccountContext, we don't really need to do this since there is
    /// one and only one CommonContext database, but for consistency, it's here.</para>
    /// <para>The syntax to create a migration is &quot;dotnet ef migrations add [name] -c CommonContext&quot;</para>
    /// <para>More info: http://benjii.me/2016/05/dotnet-ef-migrations-for-asp-net-core/ </para>
    /// </summary>
    public class MarketPlaceDbContextFactory : IDesignTimeDbContextFactory<MarketPlaceDbContext>
    {
        public MarketPlaceDbContext CreateDbContext(string[] args)
        {
            var migrationsAssembly = typeof(MarketPlaceDbContext).GetTypeInfo().Assembly.GetName().Name;

            var optionsBuilder = new DbContextOptionsBuilder<MarketPlaceDbContext>();
            optionsBuilder.UseSqlServer("Server=sqldata;Database=payscrow.marketplace.db;User Id=sa;Password=Pass@word;",
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(migrationsAssembly);
                        });

            return new MarketPlaceDbContext(optionsBuilder.Options);
        }
    }
}
