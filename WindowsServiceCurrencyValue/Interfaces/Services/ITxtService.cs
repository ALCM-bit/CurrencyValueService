﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Models;

namespace WindowsServiceCurrencyValue.Interfaces.Services
{
    public interface ITxtService
    {
        Task WriteData(List<Currency> data);
        Task WriteError();
        Task WriteStopMessage();
    }
}