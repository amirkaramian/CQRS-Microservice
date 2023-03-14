using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payscrow.WebUI.Services;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Areas.Business.Controllers.Escrow
{
    public class CurrenciesController : BaseEscrowApiController
    {
        private readonly CurrencyService _currencyService;

        public CurrenciesController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCurrenciesAsync()
        {
            var response = await _currencyService.GetCurrencyModels();

            response.EnsureSuccessStatusCode();

            var currenciesResponse = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(currenciesResponse));
        }
    }
}