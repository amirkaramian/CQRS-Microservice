using Payscrow.WebUI.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Services
{
    public class AccountSettingService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;


        public AccountSettingService(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
        }


        public async Task<HttpResponseMessage> GetAccountSettingModel()
        {
            var url = UrlsConfig.EscrowOperations.AccountSettings(_appSettings.Urls.Escrow);

            var response = await _httpClient.GetAsync(url);
            return response;
        }

        public async Task<HttpResponseMessage> ChangeCurrencyAsync(string currencyCode)
        {
            var url = UrlsConfig.EscrowOperations.ChangeCurrency(_appSettings.Urls.Escrow, currencyCode);

            var response = await _httpClient.GetAsync(url);
            return response;
        }
    }
}
