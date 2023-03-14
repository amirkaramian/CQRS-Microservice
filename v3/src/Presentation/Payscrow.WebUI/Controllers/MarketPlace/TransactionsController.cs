using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payscrow.WebUI.BindingModels.MarketPlace;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Controllers.MarketPlace
{
    public class TransactionsController : BaseMarketPlaceApiController
    {
        private readonly HttpClient _httpClient;

        public TransactionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(HttpClientNameConstants.MARKET_PLACE);
        }

        [HttpPost("start")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> StartTransactionAsync([FromBody] CreateTransactionBindingModel model)
        {
            model.ReturnUrl = Url.PageLink("/MarketPlace/Payment");

            if (HttpContext.Request.Headers.TryGetValue("BrokerApiKey", out var brokerApiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("BrokerApiKey", brokerApiKey.FirstOrDefault());
            }

            var command = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/v3/transactions/create", command);

            var createTransactionResponse = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvertHelper.TryDeserialize(createTransactionResponse));
        }

        [HttpGet("pending/{id}")]
        public async Task<ActionResult> GetPendingAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/v3/transactions/{id}");

            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()));
        }

        [HttpGet("{id}/status")]
        public async Task<ActionResult> GetStatusAsync(Guid id)
        {
            if (HttpContext.Request.Headers.TryGetValue("BrokerApiKey", out var brokerApiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("BrokerApiKey", brokerApiKey.FirstOrDefault());
            }

            var response = await _httpClient.GetAsync($"api/v3/transactions/{id}/status");

            return StatusCode((int)response.StatusCode, JsonConvertHelper.TryDeserialize(await response.Content.ReadAsStringAsync()));
        }

        [HttpGet("{id}/paymentid")]
        public async Task<ActionResult> GetPaymentIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/v3/transactions/{id}/paymentid");
            var responseString = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvertHelper.TryDeserialize(responseString));
        }
    }
}