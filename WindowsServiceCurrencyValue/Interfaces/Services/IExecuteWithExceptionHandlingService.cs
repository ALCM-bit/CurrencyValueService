using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceCurrencyValue.Interfaces.Services
{
    public interface IExecuteWithExceptionHandlingService
    {
        Task<T> RequestCentralBanckAPIServiceExecuteWithExceptionHandling<T>(Func<Task<T>> function, T defaultValue);
        Task TxtMakerServiceExecuteWithExceptionHandling(Func<Task> function);
    }
}
