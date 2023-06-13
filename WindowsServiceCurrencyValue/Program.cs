using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Configurators;
using WindowsServiceCurrencyValue.Interfaces.Services;
using WindowsServiceCurrencyValue.Services;

namespace WindowsServiceCurrencyValue
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        static void Main()
        {
            var services = new ServiceCollection();

            // Configurar os logs
            LogsConfigurator.ConfigureLogging(services);

            // Configurar as dependências
            DependencyInjectionConfigurator.ConfigureDependencyInjection(services);

            var serviceProvider = services.BuildServiceProvider();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                serviceProvider.GetRequiredService<Service1>()
            };
            ServiceBase.Run(ServicesToRun);

        }
    }
}
