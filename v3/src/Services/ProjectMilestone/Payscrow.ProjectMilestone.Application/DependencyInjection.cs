using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payscrow.ProjectMilestone.Application.Common;
using Payscrow.ProjectMilestone.Application.Common.Behaviours;
using Payscrow.ProjectMilestone.Application.Interfaces.Markers;
using System.Reflection;

namespace Payscrow.ProjectMilestone.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddHttpClient();
      

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainBadRequestHandlerBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.Scan(scan => scan
                 .FromAssemblyOf<ITransientLifetime>()
                 .AddClasses(classes => classes.AssignableTo<ITransientLifetime>())
                 .AsImplementedInterfaces()
                 .WithTransientLifetime());

            services.Scan(scan => scan
             .FromAssemblyOf<ISelfTransientLifetime>()
             .AddClasses(classes => classes.AssignableTo<ISelfTransientLifetime>())
             .AsSelf()
             .WithTransientLifetime());


            var configOptions = new ConfigOptions();
            configuration.Bind(configOptions);
            services.AddSingleton(configOptions);

            return services;
        }
    }
}
