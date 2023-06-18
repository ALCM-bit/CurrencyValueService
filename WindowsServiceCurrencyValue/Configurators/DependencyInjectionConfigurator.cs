using Microsoft.Extensions.DependencyInjection;
using WindowsServiceCurrencyValue.Interfaces.Services;
using WindowsServiceCurrencyValue.Services;

namespace WindowsServiceCurrencyValue.Configurators
{
    public class DependencyInjectionConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ITextService, TextService>();
            services.AddTransient<IRequestCentralBankAPIService, RequestCentralBankAPIService>();
            

            services.AddSingleton<MainService>(serviceProvider =>
            {
                var txtMaker = serviceProvider.GetRequiredService<ITextService>();
                var apiService = serviceProvider.GetRequiredService<IRequestCentralBankAPIService>();
                return new MainService(txtMaker, apiService);
            });
        }

    }
}
