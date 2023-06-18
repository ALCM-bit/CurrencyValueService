using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Dtos;
using WindowsServiceCurrencyValue.Models;

namespace WindowsServiceCurrencyValue.Interfaces.Services
{
    public interface IRequestCentralBankAPIService
    {
        Task<ReportDTO> GetCurrencyReport(string currencyAbbreviation);
        Task<List<AbbreviationDTO>> GetCurrencyAbbreviations();
        Task<List<Currency>> GetAllCurrencyData();
    }
}
