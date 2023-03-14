using Microsoft.AspNetCore.Mvc;
using Payscrow.PaymentInvite.Application.Queries;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Api.Controllers
{
    public class CurrenciesController : BaseApiController
    {

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(GetCurrencies.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCurrencies.QueryResult>> GetAsync()
        {
            return Ok(await Mediator.Send(new GetCurrencies.Query()));
        }
    }
}
