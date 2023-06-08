using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceCurrencyValue.Models
{
    public class DataFormat
    {
        public string simbolo { get; set; }
        public string nomeFormatado { get; set; }
        public double cotacaoCompra { get; set; }
        public double cotacaoVenda { get; set; }
        public string dataHoraCotacao { get; set; } = String.Empty;
    }
}
