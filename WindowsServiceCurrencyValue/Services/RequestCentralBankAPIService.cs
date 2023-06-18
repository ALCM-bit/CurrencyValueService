using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Dtos;
using WindowsServiceCurrencyValue.Interfaces.Services;
using WindowsServiceCurrencyValue.Models;

namespace WindowsServiceCurrencyValue.Services
{
    public class RequestCentralBankAPIService : IRequestCentralBankAPIService
    {
        private readonly IMapper _mapper;
        private readonly ITxtService _textMaker;
        public RequestCentralBankAPIService(IMapper mapper, ITxtService maker)
        {
            _mapper = mapper;
            _textMaker = maker;
        }

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

        public async Task<ReportDTO> GetCurrencyReportInformation(string currencyAbbreviation)
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

        public async Task<List<Currency>> GetAllCurenci()
        {
            var abbreviations = await GetCurrencyAbbreviations();
            var data = new List<Currency>();

            if (abbreviations is null)
            {
                return new List<Currency>() { };
            }
            else
            {
                foreach (AbbreviationDTO abbreviation in abbreviations)
                {
                    ReportDTO currencyValues = await GetCurrencyReportInformation(abbreviation.Simbolo);
                    Currency currency = _mapper.Map<Currency>(abbreviation);
                    _mapper.Map(currencyValues, currency);
                    data.Add(currency);
                }
                return data;
            }
        }
    }
}