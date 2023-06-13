﻿using System;
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
        Task<CurrencyCompleteDTO> GetCurenci(string currencyAbbreviation);
        Task<List<AbbreviationDTO>> GetCurrencyAbbreviations();
        Task<List<CurrencyCompleteDTO>> GetAllCurenci();
    }
}