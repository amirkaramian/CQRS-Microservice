using Newtonsoft.Json;
using Payscrow.Payments.Api.Application.Models;
using Payscrow.Payments.Api.Application.Utilities;
using Payscrow.Payments.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Application.Services.Flutterwave
{
    public class FlutterwaveSettlementService : ITransientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppSettings _appSettings;

        public FlutterwaveSettlementService(IHttpClientFactory httpClientFactory, AppSettings appSettings)
        {
            _httpClientFactory = httpClientFactory;
            _appSettings = appSettings;
        }

        public async Task<SettlementInitiationResponseModel> InitiateBulkTransfer(IEnumerable<SettlementAccount> settlementAccounts, string currencyCode, string title)
        {
            var result = new SettlementInitiationResponseModel();

            var _httpClient = HttpFactory.InitHttpClient(_httpClientFactory, _appSettings.Flutterwave.BaseUrl)
                                 .AddAuthorizationHeader("Bearer", _appSettings.Flutterwave.SecretKey)
                                 .AddMediaType("application/json")
                                 .AddHeader("cache-control", "no-cache");

            var data = new List<dynamic>();

            foreach (var settlementAccount in settlementAccounts)
            {
                data.Add(new
                {
                    bank_code = settlementAccount.BankCode,
                    account_number = settlementAccount.AccountNumber,
                    amount = settlementAccount.Amount,
                    currency = currencyCode,
                    narration = title
                });
            }

            var payload = new
            {
                title,
                bulk_data = data
            };

            var jsonString = JsonConvert.SerializeObject(payload);

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("bulk-transfers", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var responseObj = JsonConvert.DeserializeObject<ResponseData>(responseString);

            if (responseObj.status == "success")
            {
                result.IsInitiated = true;
                result.GatewayReference = responseObj.data?.id.ToString();
            }

            return result;
        }

        public class ResponseData
        {
            public string status { get; set; }
            public string message { get; set; }
            public Data data { get; set; }

            public class Data
            {
                public int id { get; set; }
                public DateTime created_at { get; set; }
                public string approver { get; set; }
            }
        }
    }
}