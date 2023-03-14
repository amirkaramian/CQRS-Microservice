using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payscrow.EscrowDirect.Application.Interfaces;
using System;
using System.Reflection;

namespace Payscrow.EscrowDirect.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EscrowDirectDbContext>(options =>
                   {
                       options.UseSqlServer(configuration["ConnectionString"],
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly(typeof(EscrowDirectDbContext).GetTypeInfo().Assembly.GetName().Name);
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });
                   },
                       ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                   );

            services.AddScoped<IEscrowDirectDbContext>(provider => provider.GetService<EscrowDirectDbContext>());

            return services;
        }
    }
}