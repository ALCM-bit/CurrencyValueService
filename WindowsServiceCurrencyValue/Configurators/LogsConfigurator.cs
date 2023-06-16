using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
