using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using WindowsServiceCurrencyValue.Dtos;
using WindowsServiceCurrencyValue.Models;

namespace WindowsServiceCurrencyValue.Configurators
{
    public class AutoMapperConfigurator
    {
        //Configura o AutoMapper
        public static void Configure(IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AbbreviationDTO, Currency>().ReverseMap();
                cfg.CreateMap<ReportDTO, Currency>().ReverseMap();
            });
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
