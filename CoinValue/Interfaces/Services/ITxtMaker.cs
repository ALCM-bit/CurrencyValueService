using CoinValue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinValue.Interfaces.Services
{
    public interface ITxtMaker
    {
        void WriteData(List<DataFormat> data);
    }
}
