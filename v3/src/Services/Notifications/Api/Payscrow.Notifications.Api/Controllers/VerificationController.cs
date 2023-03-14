using Microsoft.AspNetCore.Mvc;
using Payscrow.Notifications.Api.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Controllers
{
    public class VerificationController : BaseApiController
    {
        [HttpPost]
        [Route("send-deal-code")]
        [ProducesResponseType(typeof(SendCreateDealVerificationCode.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Verify([FromBody] SendCreateDealVerificationCode.Command command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
