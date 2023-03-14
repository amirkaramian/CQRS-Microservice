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
using Payscrow.EscrowDirect.Api.Infrastructure.Filters;
using Payscrow.EscrowDirect.Application;
using Payscrow.EscrowDirect.Application.IntegrationEvents;
using Payscrow.EscrowDirect.Application.IntegrationEvents.Handlers;
using Payscrow.EscrowDirect.Application.Interfaces;
using Payscrow.EscrowDirect.Data;
using Payscrow.Infrastructure.Common;
using Payscrow.Infrastructure.RabbitMQBus;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace Payscrow.EscrowDirect.Api
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
            services.AddApplicationServices(Configuration);
            services.AddPersistence(Configuration);
            services.AddIntegrationEventBus(Configuration, "EscrowDirect");
            services.AddCoreCommonServices(Configuration);
            services.AddCustomAuthentication(Configuration);

            services.AddHttpContextAccessor();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(TenantFilter));
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IEscrowDirectDbContext>())

            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payscrow.EscrowDirect.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EscrowDirectDbContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payscrow.EscrowDirect.Api v1"));
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

            eventBus.Subscribe<UserRegisteredEvent, UserRegisteredEventHandler>();
        }
    }

    internal static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            Log.Information("----- Identity Url: {IdentityUrl} For Escrow Direct -----", identityUrl);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = identityUrl;
                //options.Authority = "http://host.docker.internal:7100";
                //options.Authority = "http://localhost:7100";
                options.RequireHttpsMetadata = false;
                //options.Audience = "project_milestone";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}