using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Payscrow.Web.HttpAggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             //.ConfigureAppConfiguration(cb =>
             //{
             //    var sources = cb.Sources;
             //    sources.Insert(3, new Microsoft.Extensions.Configuration.Json.JsonConfigurationSource()
             //    {
             //        Optional = true,
             //        Path = "appsettings.json",
             //        ReloadOnChange = false
             //    });
             //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
             .UseSerilog((builderContext, config) =>
             {
                 config
                     .MinimumLevel.Information()
                     .Enrich.FromLogContext()
                     .WriteTo.Console();
             });
    }
}
