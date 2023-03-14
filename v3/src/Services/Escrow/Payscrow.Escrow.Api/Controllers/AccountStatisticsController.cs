using Microsoft.AspNetCore.Mvc;
using Payscrow.Escrow.Application.Queries.Business;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Api.Controllers
{
    public class AccountStatisticsController : BaseApiController
    {
        [HttpGet("{currencyCode}")]
        [ProducesResponseType(typeof(GetAccountStatistics.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAccountStatistics.QueryResult>> GetAsync([FromRoute] string currencyCode)
        {
            var query = new GetAccountStatistics.Query { AccountGuid = AccountId, TenantId = TenantId, CurrencyCode = currencyCode };
            return Ok(await Mediator.Send(query));
        }
    }
}