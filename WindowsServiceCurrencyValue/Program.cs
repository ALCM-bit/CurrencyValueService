using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
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
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                serviceProvider.GetRequiredService<Service1>()
            };
            ServiceBase.Run(ServicesToRun);

        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITxtMaker, TxtMakerService>();
            services.AddTransient<IRequestCentralBankAPIService, RequestCentralBankAPIService>();
            services.AddSingleton<Service1>(serviceProvider =>
            {
                var txtMaker = serviceProvider.GetRequiredService<ITxtMaker>();
                var apiService = serviceProvider.GetRequiredService<IRequestCentralBankAPIService>();
                return new Service1(txtMaker, apiService);
            });
        }
    }
}
