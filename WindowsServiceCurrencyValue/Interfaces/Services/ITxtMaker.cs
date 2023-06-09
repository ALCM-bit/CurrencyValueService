using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Models;

namespace WindowsServiceCurrencyValue.Interfaces.Services
{
    public interface ITxtMaker
    {
        Task WriteData(List<DataFormat> data);
        Task WriteStopMessage();
    }
}
