using CoinValue.Interfaces.Services;
using CoinValue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinValue.Services
{
    public class TxtMaker: ITxtMaker
    {
        private const string FilePath = "C:\\ws-workspace\\Desafio_MXM\\CoinValueService\\data.txt";

        public void WriteData(List<DataFormat> data)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, false))
            {
                foreach (DataFormat item in data)
                {
                    string line = $"{item.simbolo},{item.nomeFormatado},{item.cotacaoCompra},{item.cotacaoVenda},{item.dataHoraCotacao}";
                    writer.WriteLine(line);
                }
            }
        }
    }
}
