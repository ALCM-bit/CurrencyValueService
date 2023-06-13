using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Dtos;
using WindowsServiceCurrencyValue.Interfaces.Services;
using WindowsServiceCurrencyValue.Models;

namespace WindowsServiceCurrencyValue.Services
{
    public class RequestCentralBankAPIService: IRequestCentralBankAPIService
    {
        public async Task<CurrencyDTO> GetCurenci(string currencyAbbreviation)
        {

            string currency = currencyAbbreviation;
            string date = DateTime.Now.ToString("MM-dd-yyyy");
            string uri = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoMoedaDia(moeda=@moeda,dataCotacao=@dataCotacao)?@moeda='{currency}'&@dataCotacao='{date}'&$top=100&$orderby=dataHoraCotacao%20desc&$format=json&$select=cotacaoCompra,cotacaoVenda,dataHoraCotacao";

            HttpClient client = new HttpClient();
            var request = await client.GetAsync(uri);
            var content = await request.Content.ReadAsStringAsync();

            var currencyData = JsonConvert.DeserializeObject<CentralBankApiResponseDTO>(content);

            if (currencyData.Value.Count > 0)
            {
                return currencyData.Value[0];
            }
            else
            {
                return new CurrencyDTO { CotacaoCompra = 0, CotacaoVenda = 0 };
            }
        }

        public async Task<List<CurrencyDTO>> GetCurrencyAbbreviations()
        {
            string uri = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/Moedas?$top=100&$format=json&$select=simbolo,nomeFormatado,tipoMoeda";

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();

            var currencyData = JsonConvert.DeserializeObject<CentralBankApiResponseDTO>(content);
            var abbreviations = currencyData.Value;

            return abbreviations;
        }
        public async Task<List<CurrencyDTO>> GetAllCurenci()
        {
            var currencyData = await GetCurrencyAbbreviations();


            foreach (CurrencyDTO abbreviation in currencyData)
            {
                CurrencyDTO currencyValues = await GetCurenci(abbreviation.Simbolo);
                abbreviation.CotacaoCompra = currencyValues.CotacaoCompra;
                abbreviation.CotacaoVenda = currencyValues.CotacaoVenda;
                abbreviation.DataHoraCotacao = currencyValues.DataHoraCotacao;
            }

            return currencyData;
        }
    }
}
