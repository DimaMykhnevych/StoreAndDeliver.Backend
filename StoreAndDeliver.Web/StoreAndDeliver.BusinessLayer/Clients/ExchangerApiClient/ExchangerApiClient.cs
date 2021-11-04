using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Clients.ExchangerApiClient
{
    public class ExchangerApiClient : IExchangerApiClient
    {
        private readonly ExchangeRatesApiOptions _exchangeRatesApiOptions;
        private readonly HttpClient _httpClient;

        public ExchangerApiClient(IOptions<ExchangeRatesApiOptions> options, HttpClient httpClient)
        {
            _exchangeRatesApiOptions = options.Value;
            _httpClient = httpClient;
        }

        public async Task<ExchangerApiConvertionDto> GetCurrencyRate()
        {
            var parameters = new Dictionary<string, string> {
                { "API_KEY", _exchangeRatesApiOptions.ApiKey }
            };

            var apiRoute = GetFullApiRoute(parameters, ApiRoutes.CurrentCurrencyRate);

            var request = await _httpClient.GetAsync(apiRoute);
            var response = await request.Content.ReadAsStringAsync();
            var currencyInfo = JsonConvert.DeserializeObject<ExchangerApiConvertionDto>(response);
            return currencyInfo;

        }

        private static string GetFullApiRoute(Dictionary<string, string> parameters, string templateRoute)
        {
            return Regex.Replace(templateRoute, @"\{(.+?)\}", m => parameters[m.Groups[1].Value]);
        }
    }
}
