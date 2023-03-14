using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Payscrow.Infrastructure.RabbitMQBus;
using Payscrow.PaymentInvite.Application;
using Payscrow.PaymentInvite.Data;
using FluentValidation.AspNetCore;
using Payscrow.PaymentInvite.Application.Interfaces;
using Newtonsoft.Json.Converters;
using Payscrow.Core.Bus;
using Payscrow.PaymentInvite.Application.IntegrationEvents;
using Payscrow.PaymentInvite.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Payscrow.PaymentInvite.Api.Grpc;
using Payscrow.PaymentInvite.Api.Middleware.Grpc;
using Payscrow.PaymentInvite.Application.IntegrationEvents.Handlers.UserRegisteredEventHandlers;
using Payscrow.PaymentInvite.Application.IntegrationEvents.Handlers;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Payscrow.PaymentInvite.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;
using Payscrow.Infrastructure.Common;

namespace Payscrow.PaymentInvite.Api
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
            services.AddIntegrationEventBus(Configuration, "PaymentInvite");
            services.AddCustomAuthentication(Configuration);
            services.AddCoreCommonServices(Configuration);

            services.AddGrpc(options => {
                options.EnableDetailedErrors = true;
                //options.EnableMessageValidation();
                options.Interceptors.Add(typeof(GrpcValidationHandler));
            });
            

            //services.AddGrpcValidation();

            services.AddControllers(options => {
                //options.Filters.Add(typeof(ValidateModelStateAttribute));
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IPaymentInviteDbContext>())

            .AddNewtonsoftJson(options =>
             {
                //options.SerializerSettings.DateFormatString = "dd/MM/yyyy";
                    //options.SerializerSettings.Converters.Add(new CustomDateTimeConverter());
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    //options.SerializerSettings.Converters.Add(new ValidEnumConverter());
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;                
            });

           

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(3, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            services.AddSwaggerGen(x => x.SwaggerDoc("v3", new OpenApiInfo { Title = "Payment Invite Service", Version = "v3" }));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.Scan(scan => scan
            .FromAssemblyOf<IEventHandlerService>()
            .AddClasses(classes => classes.AssignableTo<IEventHandlerService>())
            .AsSelf()
            .WithScopedLifetime());


            services.AddDataProtection(opts =>
            {
                opts.ApplicationDiscriminator = "payscrow.paymentinvite";
            }).PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(Configuration["RedisConnectionString"]), "PaymentInvite-DataProtection-Keys");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PaymentInviteDbContext context)
        {
            context.Database.Migrate();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v3/swagger.json", "Payment Invite service V3"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<InvitesService>();

                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<UserRegisteredEvent, CreateTrader>();
            eventBus.Subscribe<PaymentVerifiedIntegrationEvent, PaymentVerifiedEventHandler>();
        }

        
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            Log.Information("----- Identity Url: {IdentityUrl} For Payment Invite", identityUrl);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                //options.Audience = "payment_invite";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };

            });


            //services.AddAuthentication("Bearer")
            //  .AddJwtBearer("Bearer", options =>
            //  {
            //      options.Authority = identityUrl;

            //      options.RequireHttpsMetadata = false;


            //      options.TokenValidationParameters = new TokenValidationParameters
            //      {
            //          ValidateAudience = false
            //      };
            //  });



            return services;
        }
    }
}
