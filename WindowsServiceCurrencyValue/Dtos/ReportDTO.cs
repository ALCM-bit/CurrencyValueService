using System;

namespace WindowsServiceCurrencyValue.Dtos
{
    public class ReportDTO
    {
        public double CotacaoCompra { get; set; }
        public double CotacaoVenda { get; set; }
        public string DataHoraCotacao { get; set; } = String.Empty;
    }
}
