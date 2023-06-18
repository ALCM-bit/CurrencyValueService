using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Dtos;
using WindowsServiceCurrencyValue.Interfaces.Services;
using WindowsServiceCurrencyValue.Models;
using WindowsServiceCurrencyValue.Services;

namespace WindowsServiceCurrencyValue.Configurators
{
    public class DependencyInjectionConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ITxtService, TxtService>();
            services.AddTransient<IRequestCentralBankAPIService, RequestCentralBankAPIService>();
            

            services.AddSingleton<MainService>(serviceProvider =>
            {
                var txtMaker = serviceProvider.GetRequiredService<ITxtService>();
                var apiService = serviceProvider.GetRequiredService<IRequestCentralBankAPIService>();
                return new MainService(txtMaker, apiService);
            });
        }

    }
}
