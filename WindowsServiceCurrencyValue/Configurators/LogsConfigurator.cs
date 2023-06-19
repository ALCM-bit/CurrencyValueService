using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.File;
using System;

namespace WindowsServiceCurrencyValue.Configurators
{
    public class LogsConfigurator
    {
        //Configurações do Serilog
        public static void Configure(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + $"Logs/Exceptions.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.AddSerilog();
            });
        }
    }
}
