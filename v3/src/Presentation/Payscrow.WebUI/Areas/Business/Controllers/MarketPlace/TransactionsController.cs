using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payscrow.WebUI.Areas.Business.Common;
using Payscrow.WebUI.Areas.Business.Dtos.MarketPlace;
using Payscrow.WebUI.Areas.Business.Dtos.MarketPlace.Binding;
using Payscrow.WebUI.Areas.Business.Services.MarketPlace;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Areas.Business.Controllers.MarketPlace
{
    public class TransactionsController : BaseMarketPlaceApiController
    {
        private readonly MarketPlaceTransactionService _marketPlaceTransactionService;
        private readonly HttpClient _httpClient;

        public TransactionsController(MarketPlaceTransactionService marketPlaceTransactionService, IHttpClientFactory httpClientFactory)
        {
            _marketPlaceTransactionService = marketPlaceTransactionService;
            _httpClient = httpClientFactory.CreateClient(HttpClientNameConstants.MARKET_PLACE);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(BaseDataTableResult<TransactionDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDatatableAsync([FromForm] DataTableRequest request, [FromQuery] string currencyCode)
        {
            return Ok(await _marketPlaceTransactionService.GetTransactionsAsync(request, currencyCode));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/v3/transactions/{id}/detail");
            var responseString = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvertHelper.TryDeserialize(responseString));
        }

        [HttpPost("{id}/dispute")]
        public async Task<IActionResult> RaiseDisputeAsync(Guid id, RaiseDisputeBindingDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/v3/transactions/{id}/dispute", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvertHelper.TryDeserialize(responseString));
        }
    }
}