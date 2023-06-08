using CoinValue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinValue.Interfaces.Services
{
    public interface IRequestCentralBankAPIService
    {
        Task<DataFormat> GetCurenci(string currencyAbbreviation);
        Task<List<DataFormat>> GetCurrencyAbbreviations();
        Task<List<DataFormat>> GetAllCurenci();
    }
}
