﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Interfaces.Services;
using WindowsServiceCurrencyValue.Models;

namespace WindowsServiceCurrencyValue.Services
{
    public class TxtMakerService : ITxtMaker
    {

        public void WriteData(List<DataFormat> data)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $"ServiceLog_{DateTime.Now.Date.ToShortDateString().Replace('/', '-')}.txt");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    string important = "Caso os valores apareçam como 0 significa que não há nenhuma atualização no dia de hoje." +
                        "Caso a informação persistir entre em contato com o suporte";
                    writer.WriteLine(important);
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Ultima pesquisa: " + DateTime.Now.ToString());
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
            catch (Exception ex)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\LogsErrors";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $"ServiceLog_{DateTime.Now.Date.ToShortDateString().Replace('/', '-')} Error.txt");
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.WriteLine("O programa parou de funcionar em: " + DateTime.Now.ToString());
                    writer.WriteLine("");
                    writer.WriteLine("--------------------------------------");
                    writer.WriteLine("");
                    writer.WriteLine("PROBLEMA: ");
                    writer.Write(ex.Message);
                }

            }

        }

        public void WriteStopMessage()
        {
            string FilePath = $"C:\\ws-workspace\\Desafio_MXM\\CoinValueService\\{DateTime.Now.Date.ToShortDateString().Replace('/', '-')} Stoped.txt";
            using (StreamWriter writer = new StreamWriter(FilePath, false))
            {
                writer.WriteLine("O serviço parou de funcionar" + DateTime.Now.ToString());
                writer.WriteLine("-----------------------------------------------------------------");
                writer.WriteLine("Caso não tenha sido parado por você, faça uma das seguintes opções:");
                writer.WriteLine("1 - Reinicie o computador;");
                writer.WriteLine("2 - Contate o suporte;");
                writer.WriteLine("-----------------------------------------------------------------");

            }
        }
    }
}
