using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payscrow.WebUI.Areas.Business.Dtos.Escrow.Binding;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Areas.Business.Controllers.Escrow
{
    public class EscrowTransactionsController : BaseEscrowApiController
    {
        private readonly HttpClient _httpClient;

        public EscrowTransactionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(HttpClientNameConstants.ESCROW);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            var url = $"api/v3/escrowtransactions/{id}";

            var response = await _httpClient.GetAsync(url);

            var responseString = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(responseString));
        }

        [HttpPost("{id}/applycode")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ApplyEscrowCodeAsync([FromRoute] Guid id, [FromBody] ApplyEscrowCodeDto model)
        {
            var url = $"api/v3/escrowtransactions/{id}/applycode";

            var jsonObj = JsonConvert.SerializeObject(model);

            var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvertHelper.TryDeserialize(responseString));
        }
    }
}