using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Payscrow.Core.Bus;
using Payscrow.Escrow.Api.Infrastructure.Filters;
using Payscrow.Escrow.Application;
using Payscrow.Escrow.Application.BackgroundJobs;
using Payscrow.Escrow.Application.IntegrationEvents;
using Payscrow.Escrow.Application.IntegrationEvents.Handlers;
using Payscrow.Escrow.Application.IntegrationEvents.Subscribing.MarketPlace;
using Payscrow.Escrow.Application.IntegrationEvents.Subscribing.Payments;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Application.Services;
using Payscrow.Escrow.Data;
using Payscrow.Infrastructure.Common;
using Payscrow.Infrastructure.RabbitMQBus;
using Serilog;
using StackExchange.Redis;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace Payscrow.Escrow.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistence(Configuration);
            services.AddApplicationServices(Configuration);
            services.AddCustomAuthentication(Configuration);
            services.AddCoreCommonServices(Configuration);

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddControllers(options =>
            {
                //options.Filters.Add(typeof(ValidateModelStateAttribute));
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(TenantFilter));
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IEscrowDbContext>());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Event Bus integration
            services.AddIntegrationEventBus(Configuration, Configuration["SubscriptionClientName"]);

            services.AddHangfire(configuration => configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
               .UseSqlServerStorage(Configuration["HangfireConnection"], new SqlServerStorageOptions
               {
                   CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                   SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                   QueuePollInterval = TimeSpan.Zero,
                   UseRecommendedIsolationLevel = true,
                   DisableGlobalLocks = true
               }));

            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, EscrowDbContext context)
        {
            context.Database.Migrate();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

            ConfigureEventBus(app);
            ConfigureHangfireJobs();
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<TransactionEscrowedEvent, TransactionEscrowedEventHandler>();
            eventBus.Subscribe<UserRegisteredEvent, UserRegisteredEventHandler>();

            // market place subscriptions
            eventBus.Subscribe<MarketPlacePaymentVerifiedEvent, MarketPlacePaymentVerifiedEventHandler>();
            eventBus.Subscribe<MarketPlaceTransactionDisputedEvent, MarketPlaceTransactionDisputedEventHandler>();

            // payments subscriptions
            eventBus.Subscribe<BulkSettlementToBankAccountsCompletedIntegrationEvent, BulkSettlementToBankAccountsCompletedIntegrationEventHandler>();
        }

        private void ConfigureHangfireJobs()
        {
            RecurringJob.RemoveIfExists(nameof(SendReleasedEscrowFundsToBeneficiaryBackgroundJob));
            RecurringJob.AddOrUpdate<SendReleasedEscrowFundsToBeneficiaryBackgroundJob>(nameof(SendReleasedEscrowFundsToBeneficiaryBackgroundJob),
                job => job.ExecuteAsync(), Cron.Hourly, TimeZoneInfo.Local);
        }
    }

    internal static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            Log.Information("----- Identity Url: {IdentityUrl} For Escrow Service", identityUrl);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "escrow";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}