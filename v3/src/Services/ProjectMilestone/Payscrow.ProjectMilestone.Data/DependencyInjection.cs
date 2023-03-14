using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payscrow.ProjectMilestone.Application.Interfaces;
using System;
using System.Reflection;

namespace Payscrow.ProjectMilestone.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddEntityFrameworkSqlServer()
                   .AddDbContext<MilestoneDbContext>(options =>
                   {
                       options.UseSqlServer(configuration["ConnectionString"],
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly(typeof(MilestoneDbContext).GetTypeInfo().Assembly.GetName().Name);
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });
                   },
                       ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                   );


            services.AddScoped<IMilestoneDbContext>(provider => provider.GetService<MilestoneDbContext>());

            return services;
        }
    }
}
