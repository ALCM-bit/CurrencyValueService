using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinValue.Models
{
    public class DataFormat
    {
        public double cotacaoCompra { get; set; }
        public double cotacaoVenda { get; set; }
        public string dataHoraCotacao { get; set; } = String.Empty;
    }
}
