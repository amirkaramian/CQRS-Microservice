using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Payscrow.Core.Bus;
using Payscrow.Identity.Api.Data;
using Payscrow.Identity.Api.Misc;
using Payscrow.Identity.Api.Models;
using Payscrow.Identity.Api.Services;
using Payscrow.Infrastructure.RabbitMQBus;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Payscrow.Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddRazorPages();

            var connectionString = Configuration.GetSection("ConnectionString").Value;

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseSqlServer(connectionString,
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(migrationsAssembly);
                            //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        })
            );

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            services.AddScoped<ApplicationUserManager>();
            services.AddScoped<ApplicationSignInManager>();

            IIdentityServerBuilder idsBuilder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
                options.Authentication.CookieSameSiteMode = SameSiteMode.Lax;
            })
            .AddInMemoryApiResources(Resources.GetApiResources())
            .AddInMemoryApiScopes(Resources.GetApiScopes())
            .AddInMemoryIdentityResources(Resources.GetIdentityResources())
            //.AddTestUsers(Users.Get())
            .AddInMemoryClients(Clients.Get(GetClientUrls()));
            //.AddAspNetIdentity<ApplicationUser>();
            //.AddSigningCredential(Certificate.Get());

            ////idsBuilder.AddSigningCredential(Certificate.Get());
            ///

            //X509Certificate2 rsaCertificate = null;
            //     X509Certificate2 rsaCertificate = CertificateProvider.CreateRsaCertificate("localhost", 10);

            //     var sp = new ServiceCollection()
            //.AddCertificateManager()
            //.BuildServiceProvider();

            //     var iec = sp.GetService<ImportExportCertificate>();

            //     string password = "1234";

            //     var rsaCertPfxBytes =
            //iec.ExportSelfSignedCertificatePfx(password, rsaCertificate);
            //     File.WriteAllBytes(Path.Combine(HostEnvironment.ContentRootPath, "Certificate/rsaCert.pfx"), rsaCertPfxBytes);

            //     try
            //     {
            //         rsaCertificate = new X509Certificate2(
            //              Path.Combine(HostEnvironment.ContentRootPath, "Certificate/rsaCert.pfx"),
            //              "1234"
            //              );
            //     }
            //     catch (Exception e)
            //     {
            //         // logs
            //     }

            //     if (string.Equals(HostEnvironment.EnvironmentName, "development", StringComparison.OrdinalIgnoreCase))
            //     {
            //         //idsBuilder.AddDeveloperSigningCredential();
            //         idsBuilder.AddSigningCredential(rsaCertificate);
            //     }
            //     else
            //     {
            //         if (rsaCertificate == null)
            //         {
            //             throw new System.Exception("Signing Credential not found");
            //         }

            //         idsBuilder.AddSigningCredential(rsaCertificate);
            //     }

            idsBuilder.AddDeveloperSigningCredential();

            //// EF client, scope, and persisted grant stores
            //idsBuilder.AddOperationalStore(options =>
            //        options.ConfigureDbContext = builder =>
            //            builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)))
            //    .AddConfigurationStore(options =>
            //        options.ConfigureDbContext = builder =>
            //            builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

            //// ASP.NET Identity integration
            idsBuilder.AddAspNetIdentity<ApplicationUser>();

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //    {
            //        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //    });

            // Event Bus integration
            services.AddIntegrationEventBus(Configuration, Configuration["SubscriptionClientName"]);

            services.Scan(scan => scan
               .FromAssemblyOf<IEventHandlerService>()
               .AddClasses(classes => classes.AssignableTo<IEventHandlerService>())
               .AsSelf()
               .WithScopedLifetime());

            services.AddTransient<IRedirectService, RedirectService>();
            services.AddTransient<ILoginService<ApplicationUser>, EFLoginService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<ITenentProvider, TenantProvider>();
            services.AddTransient<AccountService>();

            services.AddHttpContextAccessor();

            services.AddDataProtection(opts =>
            {
                opts.ApplicationDiscriminator = "payscrow.identity";
            }).PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(Configuration["RedisConnectionString"]), "Identity-DataProtection-Keys");

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.RequireHeaderSymmetry = false;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ApplicationIdentityDbContext context)
        {
            context.Database.Migrate();
            app.UseForwardedHeaders();

            if (HostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //InitializeDbTestData(app, GetClientUrls());

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Account}/{action=Login}/{id?}");
                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        }

        private Dictionary<string, string> GetClientUrls()
        {
            var clientUrls = new Dictionary<string, string>();

            clientUrls.Add("WebUI", Configuration.GetValue<string>("WebUIClient"));

            return clientUrls;
        }

        private static void InitializeDbTestData(IApplicationBuilder app, Dictionary<string, string> clientUrls)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                if (!context.Clients.Any())
                {
                    foreach (var client in Clients.Get(clientUrls))
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Resources.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var scope in Resources.GetApiScopes())
                    {
                        context.ApiScopes.Add(scope.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Resources.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}