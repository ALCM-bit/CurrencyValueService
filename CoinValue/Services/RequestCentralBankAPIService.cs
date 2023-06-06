using CoinValue.Interfaces.Services;
using CoinValue.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinValue.Services
{
    public class RequestCentralBankAPIService:IRequestCentralBankAPIService
    {
        public async Task<DataFormat> GetCurenci(string currencyAbbreviation)
        {
            //string date = Uri.EscapeDataString(DateTime.Now.ToString("dd-MM-yyyy"));
            

            string currency = Uri.EscapeUriString(currencyAbbreviation);
            string date = Uri.EscapeUriString(DateTime.Now.ToString("dd-MM-yyyy"));
            string uri = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoMoedaDia(moeda=@moeda,dataCotacao=@dataCotacao)?@moeda='{currency}'&@dataCotacao='{date}'&$top=100&$orderby=dataHoraCotacao%20desc&$format=json&$select=cotacaoCompra,cotacaoVenda,dataHoraCotacao";

            HttpClient client = new HttpClient();
            var request = await client.GetAsync(uri);
            var content = await request.Content.ReadAsStringAsync();

            var currencyData = JsonConvert.DeserializeObject<ResponseFormat>(content);

            return currencyData.value[0];
        }
    }
}
