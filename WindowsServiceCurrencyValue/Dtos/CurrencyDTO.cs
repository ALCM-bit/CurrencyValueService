using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceCurrencyValue.Dtos
{
    public class CurrencyDTO
    {
        public double CotacaoCompra { get; set; }
        public double CotacaoVenda { get; set; }
        public string DataHoraCotacao { get; set; } = String.Empty;
    }
}
