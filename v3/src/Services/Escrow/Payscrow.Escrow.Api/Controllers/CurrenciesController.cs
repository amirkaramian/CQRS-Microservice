using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payscrow.Escrow.Application.Queries.Business;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Api.Controllers
{
    [Authorize]
    public class CurrenciesController : BaseApiController
    {
        [HttpGet("")]
        [ProducesResponseType(typeof(List<GetCurrencies.CurrencyDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<GetCurrencies.CurrencyDto>>> GetAsync()
        {
            return Ok(await Mediator.Send(new GetCurrencies.Query { TenantId = TenantId }));
        }
    }
}