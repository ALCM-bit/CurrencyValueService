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
        private readonly IExecuteWithExceptionHandlingService _exceptionHandlingService;
        public RequestCentralBankAPIService(IMapper mapper, IExecuteWithExceptionHandlingService executeWithExceptionHandlingService)
        {
            _mapper = mapper;
            _exceptionHandlingService = executeWithExceptionHandlingService;
        }
        public async Task<CurrencyDTO> GetCurenci(string currencyAbbreviation)
        {
            try
            {
                string currency = currencyAbbreviation;
                string date = DateTime.Now.ToString("MM-dd-yyyy");
                string uri = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoMoedaDia(moeda=@moeda,dataCotacao=@dataCotacao)?@moeda='{currency}'&@dataCotacao='{date}'&$top=100&$orderby=dataHoraCotacao%20desc&$format=json&$select=cotacaoCompra,cotacaoVenda,dataHoraCotacao";

                HttpClient client = new HttpClient();
                var request = await client.GetAsync(uri);
                var content = await request.Content.ReadAsStringAsync();

                var jsonObject = JsonConvert.DeserializeObject<JObject>(content);
                var currencyArray = jsonObject["value"].ToObject<List<CurrencyDTO>>();

                if (currencyArray.Count > 0)
                {
                    var firstCurrency = currencyArray[0];
                    return firstCurrency;
                }
                else
                {
                    return new CurrencyDTO { CotacaoCompra = 0, CotacaoVenda = 0 };
                }

            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Problema ao acessar o EndPoint. Error: {ex.Message}");
                return new CurrencyDTO { CotacaoCompra = 0, CotacaoVenda = 0 };
            }
                
           
        }

        public async Task<List<AbbreviationDTO>> GetCurrencyAbbreviations()
        {
            try
            {
                string uri = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/Moedas?$top=100&$format=json&$select=simbolo,nomeFormatado,tipoMoeda";

                HttpClient client = new HttpClient();
                var response = await client.GetAsync(uri);
                var content = await response.Content.ReadAsStringAsync();

                var jsonObject = JsonConvert.DeserializeObject<JObject>(content);
                var abbreviations = jsonObject["value"].ToObject<List<AbbreviationDTO>>();

                return abbreviations;
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Problema ao acessar o EndPoint das Abreviações. Error: {ex.Message}");
                return new List<AbbreviationDTO>();
            }

                
            
            
        }
        public async Task<List<Currency>> GetAllCurenci()
        {
            var abbreviations = await GetCurrencyAbbreviations();

            var data = new List<Currency>();

            foreach (AbbreviationDTO abbreviation in abbreviations)
            {
                CurrencyDTO currencyValues = await GetCurenci(abbreviation.Simbolo);
                Currency currency = _mapper.Map<Currency>(abbreviation);
                _mapper.Map(currencyValues, currency);
                data.Add(currency);
            }

            return data;
        }
    }
}