using Microsoft.AspNetCore.Mvc;
using Payscrow.Notifications.Api.Application.Commands.ProjectMilestone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Controllers
{
    public class ProjectMilestoneController : BaseApiController
    {
        [HttpPost]
        [Route("send-invite")]
        [ProducesResponseType(typeof(SendProjectCreateNotification.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Verify([FromBody] SendProjectCreateNotification.Command command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
