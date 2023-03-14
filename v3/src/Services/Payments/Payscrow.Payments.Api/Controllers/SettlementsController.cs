using Microsoft.AspNetCore.Mvc;
using Payscrow.Payments.Api.Application.Commands.Business;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Controllers
{
    public class SettlementsController : BaseApiController
    {
        [HttpPost("initiate")]
        [ProducesResponseType(typeof(SendSettlementPayment.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SendSettlementPayment.CommandResult>> InitiateAsync([FromBody] SendSettlementPayment.Command command)
        {
            command.TenantId = TenantId;
            return Ok(await Mediator.Send(command));
        }
    }
}