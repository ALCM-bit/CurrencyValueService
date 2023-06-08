using CoinValue.Interfaces.Services;
using CoinValue.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

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

            if (currencyData.value.Count > 0)
            {
                return currencyData.value[0];
            }
            else
            {
                return new DataFormat { cotacaoCompra = 0, cotacaoVenda = 0};
            }
        }

        public async Task<List<DataFormat>> GetCurrencyAbbreviations()
        {
            string uri = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/Moedas?$top=100&$format=json&$select=simbolo,nomeFormatado,tipoMoeda";

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();

            var currencyData = JsonConvert.DeserializeObject<ResponseFormat>(content);
            var abbreviations = currencyData.value;

            return abbreviations;
        }
        public async Task<List<DataFormat>> GetAllCurenci()
        {
            var currencyData = await GetCurrencyAbbreviations();
           

            foreach (DataFormat abbreviation in currencyData)
            {
               DataFormat currencyValues = await GetCurenci(abbreviation.simbolo);
               abbreviation.cotacaoCompra = currencyValues.cotacaoCompra;
               abbreviation.cotacaoVenda = currencyValues.cotacaoVenda;
               abbreviation.dataHoraCotacao = currencyValues.dataHoraCotacao;
            }

            return currencyData;
        }
    }
}
