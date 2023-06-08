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
                string important = "Caso os valores apareçam como 0 significa que não há nenhuma atualização no dia de hoje." +
                    "Caso a informação persistir entre em contato com o suporte";
                writer.WriteLine(important);
                writer.WriteLine("-----------------------------------------------------------------------------");
                string header = "Simbolo | Nome Formatado | Valor Compra | Valor Venda | Data e Hora da Cotação";
                writer.WriteLine(header);
                foreach (DataFormat item in data)
                {
                    string line = $"{item.simbolo} - {item.nomeFormatado}  | {item.cotacaoCompra}" +
                        $" | {item.cotacaoVenda} | {item.dataHoraCotacao}";
                    writer.WriteLine(line);
                    writer.WriteLine("");
                }
            }
        }
    }
}
