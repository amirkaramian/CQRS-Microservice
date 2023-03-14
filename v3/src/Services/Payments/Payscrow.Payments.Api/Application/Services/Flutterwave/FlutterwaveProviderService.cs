using Newtonsoft.Json;
using Payscrow.Payments.Api.Application.Utilities;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Payscrow.Payments.Api.Application.Models;
using Payscrow.Payments.Api.Data;
using Payscrow.Payments.Api.Domain.Models;

namespace Payscrow.Payments.Api.Application.Services.Flutterwave
{
    public class FlutterwaveProviderService : ITransientService
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public FlutterwaveProviderService(AppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            _appSettings = appSettings;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FlutterwaveResponseModel> InitiateStandardPayment(string tx_ref, string amount,
                                        string redirect_url, string customerEmail, string customerPhone,
                                        string customerName, string payment_options = "card", string currency = "NGN")
        {


            var model = new FlutterwaveRequestModel()
            {
                Amount = amount,
                Currency = currency,
                TxRef = tx_ref,
                Customer = new Customer()
                {
                    Email = customerEmail,
                    Name = customerName,
                    Phonenumber = customerPhone
                },
                RedirectUrl = redirect_url,
                PaymentOptions = payment_options,
                Customizations = new Customizations()
                {
                    //Title = RaveConstant.SITE_TITLE,
                    //description = RaveConstant.SITE_DESCRIPTION,
                    //logo = RaveConstant.COY_LOGO_URL
                }
            };

            var _client = HttpFactory.InitHttpClient(_httpClientFactory, _appSettings.Flutterwave.BaseUrl)
                      .AddAuthorizationHeader("Bearer", _appSettings.Flutterwave.SecretKey)
                      .AddMediaType("application/json")
                      .AddHeader("cache-control", "no-cache");

            //Build the request body as json
            var jsonObj = JsonConvert.SerializeObject(model);

            var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            //send the request
            var response = await _client.PostAsync("payments", content);

            var json = await response.Content.ReadAsStringAsync();

            //Deserialize and send the response
            return JsonConvert.DeserializeObject<FlutterwaveResponseModel>(json);

        }


        public async Task<VerificationResponseModel> VerifyTransaction(string externalTransactionId, Payment payment)
        {
            var result = new VerificationResponseModel();

            var _client = HttpFactory.InitHttpClient(_httpClientFactory, _appSettings.Flutterwave.BaseUrl)
                     .AddAuthorizationHeader("Bearer", _appSettings.Flutterwave.SecretKey)
                     .AddMediaType("application/json")
                     .AddHeader("cache-control", "no-cache");

            //Send the request
            var response = await _client.GetAsync($"transactions/{externalTransactionId}/verify");

            var json = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<FlutterwaveVerification.Response>(json);

            if(responseObject.Status == "success")
            {
                if(responseObject.Data.Status == "successful")
                {
                    if (responseObject.Data.Amount >= payment.Amount
                        && responseObject.Data.Currency == payment.Currency?.Code)
                    {
                        result.IsPaymentVerified = true;
                    }
                }
            }


            return result;
        }
    }


    public partial class FlutterwaveRequestModel
    {
        [JsonProperty("tx_ref")]
        public string TxRef { get; set; }

        [JsonProperty("amount")]
        //[JsonConverter(typeof(ParseStringConverter))]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("redirect_url")]
        public string RedirectUrl { get; set; }

        [JsonProperty("payment_options")]
        public string PaymentOptions { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("customizations")]
        public Customizations Customizations { get; set; }
    }

    public partial class Customer
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phonenumber")]
        public string Phonenumber { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Customizations
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("logo")]
        public Uri Logo { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("consumer_id")]
        public long ConsumerId { get; set; }

        [JsonProperty("consumer_mac")]
        public string ConsumerMac { get; set; }
    }



    public partial class FlutterwaveResponseModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("link")]
        public Uri Link { get; set; }
    }
}
