using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using WindowsServiceCurrencyValue.Configurators;

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

            // Configurar o Serilog
            LogsConfigurator.Configure(services);

            // Configurar a Injeção de dependências
            DependencyInjectionConfigurator.Configure(services);

            //Configurar AutoMapper
            AutoMapperConfigurator.Configure(services);

            var serviceProvider = services.BuildServiceProvider();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                serviceProvider.GetRequiredService<MainService>()
            };
            ServiceBase.Run(ServicesToRun);

        }
    }
}
