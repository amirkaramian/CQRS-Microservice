using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payscrow.MarketPlace.Application.Common;
using Payscrow.MarketPlace.Application.Common.Behaviours;
using Payscrow.MarketPlace.Application.Data;
using Payscrow.MarketPlace.Application.IntegrationEvents;
using Payscrow.MarketPlace.Application.IntegrationEvents.Subscribing;
using Payscrow.MarketPlace.Application.Interfaces;
using Payscrow.MarketPlace.Application.Services;
using System;
using System.Reflection;

namespace Payscrow.MarketPlace.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MarketPlaceDbContext>(options =>
                   {
                       options.UseSqlServer(configuration["ConnectionString"],
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly(typeof(MarketPlaceDbContext).GetTypeInfo().Assembly.GetName().Name);
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });
                   },
                       ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                   );

            services.AddScoped<IMarketPlaceDbContext>(provider => provider.GetService<MarketPlaceDbContext>());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainBadRequestHandlerBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            var configSettings = new ConfigSetting();
            configuration.Bind(configSettings);
            services.AddSingleton(configSettings);

            services.AddTransient<IChargeService, ChargeService>();

            services.AddTransient<PaymentVerifiedIntegrationEventHandler>();
            services.AddTransient<EscrowCodeAppliedEventHandler>();
            services.AddTransient<BulkSettlementToBankAccountsCompletedIntegrationEventHandler>();

            return services;
        }
    }
}