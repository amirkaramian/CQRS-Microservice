using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Payscrow.Infrastructure.Common;
using Payscrow.WebUI.Areas.Business.Services.PaymentInvite;
using Payscrow.WebUI.Infrastructure;
using Payscrow.WebUI.Services;
using StackExchange.Redis;

namespace Payscrow.WebUI
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
            services.AddCoreCommonServices(Configuration);

            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);
            services.AddSingleton(appSettings);

            services.AddHttpClient(HttpClientNameConstants.MARKET_PLACE,
                options => options.BaseAddress = new Uri(appSettings.Urls.MarketPlace))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient(HttpClientNameConstants.PAYMENTS,
                options => options.BaseAddress = new Uri(appSettings.Urls.Payments))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient(HttpClientNameConstants.ESCROW,
                options => options.BaseAddress = new Uri(appSettings.Urls.Escrow))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<CurrencyService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<AccountSettingService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<UserService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<DealBusinessService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<TransactionBusinessService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<PaymentInviteService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCustomAuthentication(Configuration);

            services.Scan(scan => scan
                .FromAssemblyOf<ITransientLifetime>()
                .AddClasses(classes => classes.AssignableTo<ITransientLifetime>())
                .AsSelf()
                .WithTransientLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<ITransientLifetime>()
                .AddClasses(classes => classes.AssignableTo<ITransientLifetime>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.AddScoped<WebWorkContext>();

            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeAreaFolder("Business", "/", "RegisteredOnly");
            });

            services.AddControllers()
              .AddNewtonsoftJson(options =>
              {
                  //options.SerializerSettings.DateFormatString = "dd/MM/yyyy";
                  options.SerializerSettings.Converters.Add(new StringEnumConverter());
                  options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
              });

            services.AddControllersWithViews();

            services.AddRouting(options => options.LowercaseUrls = true);

            //services.Configure<ForwardedHeadersOptions>(options =>
            //{
            //    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            //    //options.RequireHeaderSymmetry = false;
            //    //options.KnownNetworks.Clear();
            //    //options.KnownProxies.Clear();
            //});
            IdentityModelEventSource.ShowPII = true;

            //services.AddDataProtection(opts =>
            //{
            //    opts.ApplicationDiscriminator = "payscrow.webui";
            //}).PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(Configuration["RedisConnectionString"]), "WebUI-DataProtection-Keys");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseForwardedHeaders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
                //app.UseHttpsRedirection();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }

    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            var callBackUrl = configuration.GetValue<string>("CallBackUrl");
            var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

            // Add Authentication services
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(setup =>
            {
                setup.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                setup.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime);
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = identityUrl?.ToString();
                options.SignedOutRedirectUri = callBackUrl?.ToString();
                options.ClientId = "webUI";
                options.ClientSecret = "secret";
                options.ResponseType = "code id_token";
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.RequireHttpsMetadata = false;
                options.Scope.Add("openid");
                options.Scope.Add("profile");

                //options.Scope.Add("locations");
                //options.Scope.Add("webshoppingagg");
                //options.Scope.Add("orders.signalrhub");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RegisteredOnly", x =>
                {
                    x.AddAuthenticationSchemes(OpenIdConnectDefaults.AuthenticationScheme);
                    x.RequireAuthenticatedUser();
                });
            });

            return services;
        }
    }
}