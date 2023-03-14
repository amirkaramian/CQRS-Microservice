using Microsoft.AspNetCore.Mvc;
using Payscrow.EscrowDirect.Api.Infrastructure.Attributes;
using Payscrow.EscrowDirect.Application.Queries.Currencies;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.EscrowDirect.Api.Controllers
{
    public class CurrenciesController : BaseApiController
    {
        [HttpGet]
        [Route(""), ApiKey]
        [ProducesResponseType(typeof(GetCurrencies.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCurrencies.QueryResult>> GetAsync()
        {
            return Ok(await Mediator.Send(new GetCurrencies.Query()));
        }
    }
}
