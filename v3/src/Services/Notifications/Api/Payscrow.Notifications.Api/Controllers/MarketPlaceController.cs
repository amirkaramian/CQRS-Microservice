using Microsoft.AspNetCore.Mvc;
using Payscrow.Notifications.Api.Application.Commands.MarketPlace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Controllers
{
    public class MarketPlaceController : BaseApiController
    {
        [HttpPost("email-verify-code")]
        [ProducesResponseType(typeof(SendCustomerEmailVerificationCode.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Verify([FromBody] SendCustomerEmailVerificationCode.Command command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
