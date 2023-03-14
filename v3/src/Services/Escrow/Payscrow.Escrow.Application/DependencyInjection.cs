using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payscrow.Escrow.Application.Common;
using Payscrow.Escrow.Application.Common.Behaviours;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Application.Services;
using StackExchange.Redis;
using System.Reflection;

namespace Payscrow.Escrow.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddHttpClient();

            var configOptions = new ConfigOptions();
            configuration.Bind(configOptions);
            services.AddSingleton(configOptions);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainBadRequestHandlerBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddTransient<ICacheManager, RedisDistributedCacheManager>();

            services.AddSingleton<IConnectionMultiplexer>(x =>
                ConnectionMultiplexer.Connect(configuration.GetSection("RedisConnection").Value));

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

            return services;
        }
    }
}