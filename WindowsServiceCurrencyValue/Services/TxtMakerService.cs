using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Helpers;
using WindowsServiceCurrencyValue.Interfaces.Services;
using WindowsServiceCurrencyValue.Models;

namespace WindowsServiceCurrencyValue.Services
{
    public class TxtMakerService : ITxtMaker
    {

        public async Task WriteData(List<Currency> data)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cotações");
                DirectoryHelper.CreateDirectoryIfNotExists(path);
                string filePath = Path.Combine(path, $"Cotação de {DateTime.Now.Date.ToShortDateString().Replace('/', '-')}.txt");

                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    string important = "Caso os valores apareçam como 0 significa que não há nenhuma atualização no dia de hoje." +
                        "Caso a informação persistir entre em contato com o suporte";
                    await writer.WriteLineAsync(important);
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    await writer.WriteLineAsync("Ultima pesquisa: " + DateTime.Now.ToString());
                    string header = "Simbolo | Nome Formatado | Valor Compra | Valor Venda | Data e Hora da Cotação";
                    await writer.WriteLineAsync(header);
                    foreach (Currency item in data)
                    {
                        string line = $"{item.Simbolo} - {item.NomeFormatado}  | {item.CotacaoCompra}" +
                            $" | {item.CotacaoVenda} | {item.DataHoraCotacao}";
                        await writer.WriteLineAsync(line);
                        await writer.WriteLineAsync("");
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Problema ao gravar as Cotações. Error: {ex.Message}");
            }
           
        }

        public async Task WriteData()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cotações");
                DirectoryHelper.CreateDirectoryIfNotExists(path);
                string filePath = Path.Combine(path, $"Cotação de {DateTime.Now.Date.ToShortDateString().Replace('/', '-')}.txt");

                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    await writer.WriteLineAsync("Ultima pesquisa: " + DateTime.Now.ToString());
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    await writer.WriteLineAsync("A pesquisa não retornou nada. Isso pode acontecer caso: ");
                    await writer.WriteLineAsync("1 - O computador está sem internet");
                    await writer.WriteLineAsync("2 - Problema interno");
                    await writer.WriteLineAsync("-----------------------------------------------------------------------------");
                    await writer.WriteLineAsync("Recomenda-se aguardar um pouco e tentar reabrir o arquivo. " +
                        "Caso o erro persista: Contate o Suporte");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Problema ao gravar aviso de problema de acesso. Error: {ex.Message}");
            }

        }

        public async Task WriteStopMessage()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StopAlerts");
                DirectoryHelper.CreateDirectoryIfNotExists(path);
                string filePath = Path.Combine(path, $" Parada registrada {DateTime.Now.Date.ToShortDateString().Replace('/', '-')}.txt");
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    await writer.WriteLineAsync("O serviço parou de funcionar" + DateTime.Now.ToString());
                    await writer.WriteLineAsync("-----------------------------------------------------------------");
                    await writer.WriteLineAsync("Caso não tenha sido parado por você, faça uma das seguintes opções:");
                    await writer.WriteLineAsync("1 - Reinicie o computador;");
                    await writer.WriteLineAsync("2 - Contate o suporte;");
                    await writer.WriteLineAsync("-----------------------------------------------------------------");
                }

            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Problema ao Gravar o arquivo de parada. Error: {ex.Message}");
            }
            
        }
    }
}
