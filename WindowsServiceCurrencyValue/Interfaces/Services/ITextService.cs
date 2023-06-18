using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Models;

namespace WindowsServiceCurrencyValue.Interfaces.Services
{
    public interface ITextService
    {
        Task WriteData(List<Currency> data);
        Task WriteError();
        Task WriteStopMessage();
    }
}
