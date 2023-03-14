using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payscrow.Core.Interfaces;
using Payscrow.Infrastructure.Common.Services;

namespace Payscrow.Infrastructure.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantService, TenantService>();
            services.AddTransient<IIdentityService, IdentityService>();

            var configSettings = new ConfigSettings();
            configuration.Bind(configSettings);
            services.AddSingleton(configSettings);

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
