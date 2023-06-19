using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Dtos;
using WindowsServiceCurrencyValue.Interfaces.Services;
using WindowsServiceCurrencyValue.Models;

namespace WindowsServiceCurrencyValue.Services
{
    //Classe responsável pelas requisições para a API
    public class RequestCentralBankAPIService : IRequestCentralBankAPIService
    {
        private readonly IMapper _mapper;
        public RequestCentralBankAPIService(IMapper mapper)
        {
            _mapper = mapper;
        }

        //Faz a requisição para o endpoint que contem as abbreviações e retorna a lista com todas.
        public async Task<List<AbbreviationDTO>> GetCurrencyAbbreviations()
        {
            string uri = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/Moedas?$top=100&$format=json&$select=simbolo,nomeFormatado,tipoMoeda";

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject<JObject>(content);
            var abbreviations = jsonObject["value"].ToObject<List<AbbreviationDTO>>();

            return abbreviations;
        }

        /*Faz a requisição para o endpoint com os valores de compra e venda das moedas monitoradas pelo Baco Central
         * e retorna esses valores encontrados. Caso o retorno seja vazio, os valores são retornados como 0
         */
        public async Task<ReportDTO> GetCurrencyReport(string currencyAbbreviation)
        {
            string abbreviation = currencyAbbreviation;
            string date = DateTime.Now.ToString("MM-dd-yyyy");
            string uri = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoMoedaDia(moeda=@moeda,dataCotacao=@dataCotacao)?@moeda='{abbreviation}'&@dataCotacao='{date}'&$top=100&$orderby=dataHoraCotacao%20desc&$format=json&$select=cotacaoCompra,cotacaoVenda,dataHoraCotacao";

            HttpClient client = new HttpClient();
            var request = await client.GetAsync(uri);
            var content = await request.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject<JObject>(content);
            var currencyArray = jsonObject["value"].ToObject<List<ReportDTO>>();

            if (currencyArray.Count > 0)
            {
                var firstCurrency = currencyArray[0];
                return firstCurrency;
            }
            else
            {
                return new ReportDTO { CotacaoCompra = 0, CotacaoVenda = 0 };
            }
        }

        /*Utiliza os metodos responsáveis pelas requisições para gerar a lista com as informações 
         * desejadas pelo usuário.
         */
        public async Task<List<Currency>> GetAllCurrencyData()
        {
            var abbreviations = await GetCurrencyAbbreviations();
            var data = new List<Currency>();

            foreach (AbbreviationDTO abbreviation in abbreviations)
            {
                ReportDTO currencyValues = await GetCurrencyReport(abbreviation.Simbolo);
                Currency currency = _mapper.Map<Currency>(abbreviation);
                _mapper.Map(currencyValues, currency);
                data.Add(currency);
            }
            return data;
        }
    }
}