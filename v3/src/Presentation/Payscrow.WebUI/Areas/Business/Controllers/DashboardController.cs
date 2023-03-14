using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payscrow.WebUI.Areas.Business.Controllers.MarketPlace;
using System.Net.Http;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Areas.Business.Controllers
{
    [Route("api/v3/business/dashboard")]
    [ApiController, Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly HttpClient _escrowHttpClient;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _escrowHttpClient = httpClientFactory.CreateClient(HttpClientNameConstants.ESCROW);
        }

        [HttpGet("{currencyCode}")]
        public async Task<ActionResult> GetAsync([FromRoute] string currencyCode)
        {
            var response = await _escrowHttpClient.GetAsync($"api/v3/accountstatistics/{currencyCode}");

            return StatusCode((int)response.StatusCode, JsonConvertHelper.TryDeserialize(await response.Content.ReadAsStringAsync()));
        }
    }
}