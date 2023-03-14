using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payscrow.PaymentInvite.Application.Common;
using Payscrow.PaymentInvite.Application.Common.Behaviours;
using Payscrow.PaymentInvite.Application.Interfaces;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Reflection;

namespace Payscrow.PaymentInvite.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.Unauthorized)
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(2));

            services.AddHttpClient("payment-invite")
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(retryPolicy);


            var configOptions = new ConfigOptions();
            configuration.Bind(configOptions);
            services.AddSingleton(configOptions);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

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


            return services;
        }
    }
}
