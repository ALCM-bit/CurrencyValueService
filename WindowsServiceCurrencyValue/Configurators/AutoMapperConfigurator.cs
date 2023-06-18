using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Dtos;
using WindowsServiceCurrencyValue.Models;

namespace WindowsServiceCurrencyValue.Configurators
{
    public class AutoMapperConfigurator
    {
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
