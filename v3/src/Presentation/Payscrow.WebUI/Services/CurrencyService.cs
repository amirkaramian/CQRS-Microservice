using Payscrow.WebUI.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;


        public CurrencyService(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
        }

        public async Task<HttpResponseMessage> GetCurrencyModels()
        {
            var url = UrlsConfig.EscrowOperations.Currencies(_appSettings.Urls.Escrow);

            var response = await _httpClient.GetAsync(url);
            return response;
        }
    }
}
