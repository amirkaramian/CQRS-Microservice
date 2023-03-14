using System;
using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Payscrow.Core.Bus;
using Payscrow.Infrastructure.RabbitMQBus;
using Payscrow.Notifications.Api.Application.Commands;
using Payscrow.Notifications.Api.Application.IntegrationEvents;
using Payscrow.Notifications.Api.Application.IntegrationEvents.Handlers;
using Payscrow.Notifications.Api.Application.IntegrationEvents.Handlers.DealCreated;
using Payscrow.Notifications.Api.Application.IntegrationEvents.Identity;
using Payscrow.Notifications.Api.Application.IntegrationEvents.MarketPlace;
using Payscrow.Notifications.Api.Application.Interfaces;
using Payscrow.Notifications.Api.Data;
using Payscrow.Notifications.Api.Infrastructure.Email.Mailtrap;
using Payscrow.Notifications.Api.Infrastructure.SendGrid;
using Payscrow.Notifications.Api.Interfaces;

namespace Payscrow.Notifications.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NotificationDbContext>(options =>
                   {
                       options.UseSqlServer(Configuration["ConnectionString"],
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly(typeof(NotificationDbContext).GetTypeInfo().Assembly.GetName().Name);
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });
                   },
                       ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                   );

            services.AddScoped<INotificationDbContext>(provider => provider.GetService<NotificationDbContext>());

            if (_env.IsDevelopment())
            {
                services.AddTransient<IEmailNotificationService, MailtrapEmailNotificationService>();
            }
            else
            {
                services.AddTransient<IEmailNotificationService, SendGridEmailNotificationService>();
            }

            services.AddControllers()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BaseCommandResult>());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddIntegrationEventBus(Configuration, Configuration["SubscriptionClientName"]);

            services.Scan(scan => scan
                 .FromAssemblyOf<ITransientService>()
                 .AddClasses(classes => classes.AssignableTo<ITransientService>())
                 .AsImplementedInterfaces()
                 .WithTransientLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<IEventHandlerService>()
                .AddClasses(classes => classes.AssignableTo<IEventHandlerService>())
                .AsSelf()
                .WithScopedLifetime());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, NotificationDbContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<UserRegisteredEvent, UserRegisteredEventHandler>();
            eventBus.Subscribe<DealCreatedEvent, SendDealCreationCompletionEmail>();
            eventBus.Subscribe<MarketPlacePaymentVerifiedEvent, MarketPlacePaymentVerifiedEventHandler>();
        }
    }
}