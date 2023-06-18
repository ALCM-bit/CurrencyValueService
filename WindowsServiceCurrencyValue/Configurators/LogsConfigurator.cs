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
        public static void Configure(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + $"Logs/Exceptions {DateTime.Now.Date.ToShortDateString().Replace('/', '-')}.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.AddSerilog();
            });
        }
    }
}
