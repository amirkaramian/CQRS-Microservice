using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payscrow.Escrow.Application.Commands.EscrowTransactions;
using Payscrow.Escrow.Application.Queries.Business.EscrowTransactions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Api.Controllers
{
    public class EscrowTransactionsController : BaseApiController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetEscrowTransaction.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetEscrowTransaction.QueryResult>> GetAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetEscrowTransaction.Query { TenantId = TenantId, TransactionGuid = id, AccountGuid = AccountId }));
        }

        [Authorize]
        [HttpPost("{id}/applycode")]
        [ProducesResponseType(typeof(ApplyEscrowCode.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApplyEscrowCode.CommandResult>> ApplyCodeAsync([FromRoute] Guid id, [FromBody] ApplyEscrowCode.Command command)
        {
            command.TransactionGuid = id;
            command.TenantId = TenantId;
            command.AccountId = AccountId;
            return Ok(await Mediator.Send(command));
        }
    }
}