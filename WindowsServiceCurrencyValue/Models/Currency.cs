using System;

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
