using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceCurrencyValue.Models
{
    public class Currency
    {
        public string Simbolo { get; set; }
        public string NomeFormatado { get; set; }
        public double CotacaoCompra { get; set; }
        public double CotacaoVenda { get; set; }
        public string DataHoraCotacao { get; set; } = String.Empty;
    }
}
