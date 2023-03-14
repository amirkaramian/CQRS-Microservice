using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Controllers.Escrow
{
    public class CurrenciesController : BaseApiController
    {
        private readonly HttpClient _httpClient;

        public CurrenciesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(HttpClientNameConstants.ESCROW);
        }

        [HttpGet("")]
        public async Task<ActionResult> GetAsync()
        {
            var response = await _httpClient.GetAsync($"api/v3/currencies");

            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()));
        }
    }
}