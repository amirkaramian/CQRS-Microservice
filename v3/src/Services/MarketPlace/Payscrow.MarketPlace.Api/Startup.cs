using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Payscrow.Core.Bus;
using Payscrow.Infrastructure.Common;
using Payscrow.Infrastructure.RabbitMQBus;
using Payscrow.MarketPlace.Api.Infrastructure.Filters;
using Payscrow.MarketPlace.Application;
using Payscrow.MarketPlace.Application.Data;
using Payscrow.MarketPlace.Application.IntegrationEvents;
using Payscrow.MarketPlace.Application.IntegrationEvents.Subscribing;
using Payscrow.MarketPlace.Application.Interfaces;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace Payscrow.MarketPlace.Api
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
            services.AddApplication(Configuration);
            services.AddIntegrationEventBus(Configuration, "MarketPlace");
            services.AddCoreCommonServices(Configuration);
            services.AddCustomAuthentication(Configuration);

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(TenantFilter));
            })
           .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IMarketPlaceDbContext>())

           .AddNewtonsoftJson(options =>
           {
               options.SerializerSettings.Converters.Add(new StringEnumConverter());
               options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
           });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);
            services.AddSingleton(appSettings);

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddHttpClient();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v3", new OpenApiInfo { Title = "Payscrow.MarketPlace.Api", Version = "v3" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MarketPlaceDbContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payscrow.MarketPlace.Api v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<PaymentVerifiedIntegrationEvent, PaymentVerifiedIntegrationEventHandler>();
            eventBus.Subscribe<EscrowCodeAppliedEvent, EscrowCodeAppliedEventHandler>();
            eventBus.Subscribe<BulkSettlementToBankAccountsCompletedIntegrationEvent, BulkSettlementToBankAccountsCompletedIntegrationEventHandler>();
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

            Log.Information("----- Identity Url: {IdentityUrl} For Market Place -----", identityUrl);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "market_place";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}