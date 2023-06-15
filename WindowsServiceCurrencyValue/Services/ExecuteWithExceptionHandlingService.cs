using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Interfaces.Services;

namespace WindowsServiceCurrencyValue.Services
{
    public class ExecuteWithExceptionHandlingService : IExecuteWithExceptionHandlingService
    {

        public async Task<T> RequestCentralBanckAPIServiceExecuteWithExceptionHandling<T>(Func<Task<T>> function, T defaultValue) 
        {
            try
            {
                return await function();
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Error: {ex.Message}");
                return defaultValue;
            }
        }

        public async Task TxtMakerServiceExecuteWithExceptionHandling(Func<Task> function)
        {
            try
            {
                await function();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error: {ex.Message}");
            }
        }


    }
}
