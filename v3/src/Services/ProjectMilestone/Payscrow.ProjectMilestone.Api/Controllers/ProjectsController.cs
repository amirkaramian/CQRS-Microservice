using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payscrow.ProjectMilestone.Application.Commands.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.ProjectMilestone.Api.Controllers
{    
    public class ProjectsController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreateProject.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateProject.CommandResult>> Create([FromBody] CreateProject.Command command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
