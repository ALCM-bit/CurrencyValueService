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
            
            string currency = currencyAbbreviation;
            string date = DateTime.Now.ToString("MM-dd-yyyy");
            string uri = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoMoedaDia(moeda=@moeda,dataCotacao=@dataCotacao)?@moeda='{currency}'&@dataCotacao='{date}'&$top=100&$orderby=dataHoraCotacao%20desc&$format=json&$select=cotacaoCompra,cotacaoVenda,dataHoraCotacao";

            HttpClient client = new HttpClient();
            var request = await client.GetAsync(uri);
            var content = await request.Content.ReadAsStringAsync();

            var currencyData = JsonConvert.DeserializeObject<ResponseFormat>(content);

            return currencyData.value[0];
        }

        public async Task<List<DataFormat>> GetAllCurenci()
        {
            string[] curencies = {"EUR", "USD", "AUD"};
            List<DataFormat> data = new List<DataFormat>();

            foreach (string currencyAbbreviation in curencies)
            {
               DataFormat currencyData = await GetCurenci(currencyAbbreviation);
               data.Add(currencyData);
            }

            return data;
        }
    }
}
