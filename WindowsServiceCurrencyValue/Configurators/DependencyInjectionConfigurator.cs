using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddTransient<ITxtMaker, TxtMakerService>();
            services.AddTransient<IRequestCentralBankAPIService, RequestCentralBankAPIService>();

            // Configurar o AutoMapper
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CurrencyDTO, Currency>().ReverseMap();
            });
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<Service1>(serviceProvider =>
            {
                var txtMaker = serviceProvider.GetRequiredService<ITxtMaker>();
                var apiService = serviceProvider.GetRequiredService<IRequestCentralBankAPIService>();
                return new Service1(txtMaker, apiService, mapper);
            });
        }

    }
}
