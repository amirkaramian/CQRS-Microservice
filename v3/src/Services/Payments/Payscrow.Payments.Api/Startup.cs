using System;
using System.Reflection;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Payscrow.Infrastructure.RabbitMQBus;
using Payscrow.Payments.Api.Application.BackgroundJobs;
using Payscrow.Payments.Api.Application.Common.Behaviours;
using Payscrow.Payments.Api.Application.Services;
using Payscrow.Payments.Api.Data;
using Payscrow.Payments.Api.Infrastructure.Filters;

namespace Payscrow.Payments.Api
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
            services.AddDbContext<PaymentsDbContext>(options =>
                  {
                      options.UseSqlServer(Configuration["ConnectionString"],
                          sqlServerOptionsAction: sqlOptions =>
                          {
                              sqlOptions.MigrationsAssembly(typeof(PaymentsDbContext).GetTypeInfo().Assembly.GetName().Name);
                              sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                          });
                  },
                      ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                  );

            // Event Bus integration
            services.AddIntegrationEventBus(Configuration, Configuration["SubscriptionClientName"]);

            services.Scan(scan => scan
                .FromAssemblyOf<ITransientService>()
                .AddClasses(classes => classes.AssignableTo<ITransientService>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.Scan(scan => scan
              .FromAssemblyOf<ITransientService>()
              .AddClasses(classes => classes.AssignableTo<ITransientService>())
              .AsSelf()
              .WithTransientLifetime());

            services.Scan(scan => scan
              .FromAssemblyOf<ISelfTransientLifetime>()
              .AddClasses(classes => classes.AssignableTo<ISelfTransientLifetime>())
              .AsSelf()
              .WithTransientLifetime());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainBadRequestHandlerBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);
            services.AddSingleton(appSettings);

            services.AddHttpClient();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(TenantFilter));
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PaymentsDbContext>());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PaymentsDbContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

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

            ConfigureHangfireJobs();
        }

        private void ConfigureHangfireJobs()
        {
            RecurringJob.RemoveIfExists(nameof(FlutterwaveTransferStatusCheckerBackgroundJob));
            RecurringJob.AddOrUpdate<FlutterwaveTransferStatusCheckerBackgroundJob>(nameof(FlutterwaveTransferStatusCheckerBackgroundJob),
                job => job.ExecuteAsync(), Cron.Hourly, TimeZoneInfo.Local);
        }
    }
}