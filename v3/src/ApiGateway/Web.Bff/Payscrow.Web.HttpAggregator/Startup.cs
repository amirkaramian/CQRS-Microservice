using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Payscrow.Web.HttpAggregator.Config;
using Microsoft.AspNetCore.Http;
using Payscrow.Web.HttpAggregator.Filters;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Payscrow.Web.HttpAggregator.Infrastructure;
using Payscrow.Web.HttpAggregator.Services;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Payscrow.Web.HttpAggregator
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
            // services.AddHealthChecks()
            //.AddCheck("self", () => HealthCheckResult.Healthy())
            //.AddUrlGroup(new Uri(Configuration["CatalogUrlHC"]), name: "catalogapi-check", tags: new string[] { "catalogapi" })
            //.AddUrlGroup(new Uri(Configuration["OrderingUrlHC"]), name: "orderingapi-check", tags: new string[] { "orderingapi" })
            //.AddUrlGroup(new Uri(Configuration["BasketUrlHC"]), name: "basketapi-check", tags: new string[] { "basketapi" })
            //.AddUrlGroup(new Uri(Configuration["IdentityUrlHC"]), name: "identityapi-check", tags: new string[] { "identityapi" })
            //.AddUrlGroup(new Uri(Configuration["MarketingUrlHC"]), name: "marketingapi-check", tags: new string[] { "marketingapi" })
            //.AddUrlGroup(new Uri(Configuration["PaymentUrlHC"]), name: "paymentapi-check", tags: new string[] { "paymentapi" })
            //.AddUrlGroup(new Uri(Configuration["LocationUrlHC"]), name: "locationapi-check", tags: new string[] { "locationapi" });




            services.AddCustomMvc(Configuration)
                .AddCustomAuthentication(Configuration)
                //.AddDevspaces()
                .AddApplicationServices();

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(3, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v3/swagger.json", "Aggregator for Web Clients"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });         
        }


    }

    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = configuration.GetValue<string>("urls:identity");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "webagg";
            });

            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<UrlsConfig>(configuration.GetSection("urls"));

            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();

                options.SwaggerDoc("v3", new OpenApiInfo
                {
                    Title = "Aggregator for Web Clients",
                    Version = "v3",
                    Description = "Aggregator for Web Clients"
                });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize"),
                            TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token"),

                            Scopes = new Dictionary<string, string>()
                            {
                                { "api.read", "Aggregator for Web Clients" }
                            }
                        }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }


        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //register delegating handlers
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddTransient<IPaymentInviteService, PaymentInviteService>();

            //register http services

            services.AddHttpClient<IPaymentInviteService, PaymentInviteService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddTransient<IIdentityService, IdentityService>();
            //.AddDevspacesSupport();

            //services.AddHttpClient<ICatalogService, CatalogService>()
            //    .AddDevspacesSupport();

            //services.AddHttpClient<IOrderApiClient, OrderApiClient>()
            //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //    .AddDevspacesSupport();

            //services.AddHttpClient<IOrderingService, OrderingService>()
            //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //    .AddDevspacesSupport();

            return services;
        }
    }
}
