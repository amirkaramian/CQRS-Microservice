using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payscrow.Escrow.Application.Commands.Settings;
using Payscrow.Escrow.Application.Queries;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Api.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(GetUser.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetUser.QueryResult>> GetAsync()
        {
            var query = new GetUser.Query { TenantId = TenantId, UserGuid = UserGuid };

            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(UpdateUser.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateUser.CommandResult>> Create([FromBody] UpdateUser.Command command)
        {
            command.TenantId = TenantId;
            command.UserGuid = UserGuid;

            return Ok(await Mediator.Send(command));
        }
    }
}