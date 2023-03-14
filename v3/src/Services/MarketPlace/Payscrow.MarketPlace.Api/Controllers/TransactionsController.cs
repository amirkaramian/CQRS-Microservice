using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payscrow.MarketPlace.Api.Infrastructure.Attributes;
using Payscrow.MarketPlace.Application.Commands.Business.Transactions;
using Payscrow.MarketPlace.Application.Queries.Transactions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Api.Controllers
{
    public class TransactionsController : BaseApiController
    {
        [HttpPost("create"), ApiKey]
        [ProducesResponseType(typeof(CreateTransaction.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateTransaction.CommandResult>> Create([FromBody] CreateTransaction.Command command)
        {
            if (HttpContext.Items.TryGetValue("broker_account_id", out object brokerAccountId)
                && HttpContext.Items.TryGetValue("broker_name", out object brokerName))
            {
                command.BrokerAccountId = brokerAccountId.ToString().ToGuid();
                command.BrokerName = brokerName.ToString();
            }
            command.TenantId = TenantId;
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpPost("")]
        [ProducesResponseType(typeof(GetTransactions.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTransactions.QueryResult>> GetListAsync([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string currencyCode, [FromBody] GetTransactions.Query.SearchParameter search)
        {
            return Ok(await Mediator.Send(new GetTransactions.Query { AccountId = AccountId, TenantId = TenantId, Page = page, PageSize = pageSize, CurrencyCode = currencyCode, Search = search }));
        }

        [Authorize]
        [HttpGet("{id}/detail")]
        [ProducesResponseType(typeof(GetTransactionDetail.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTransactionDetail.QueryResult>> GetDetailAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetTransactionDetail.Query { TransactionId = id, AccountId = AccountId, TenantId = TenantId }));
        }

        [Authorize]
        [HttpPost("{id}/dispute")]
        [ProducesResponseType(typeof(RaiseTransactionDispute.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RaiseTransactionDispute.CommandResult>> RaiseDisputeAsync([FromRoute] Guid id, [FromBody] RaiseTransactionDispute.Command command)
        {
            command.TransactionId = id;
            command.TenantId = TenantId;
            command.AccountId = AccountId;
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetPendingTransactionDetail.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPendingTransactionDetail.QueryResult>> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetPendingTransactionDetail.Query { TransactionId = id, TenantId = TenantId }));
        }

        [HttpGet("{id}/status"), ApiKey]
        [ProducesResponseType(typeof(GetTransactionStatus.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTransactionStatus.QueryResult>> GetStatus([FromRoute] Guid id)
        {
            var query = new GetTransactionStatus.Query { TransactionId = id, TenantId = TenantId };

            if (HttpContext.Items.TryGetValue("broker_account_id", out object brokerAccountId))
            {
                query.BrokerAccountId = brokerAccountId.ToString().ToGuid();
            }

            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}/paymentid")]
        [ProducesResponseType(typeof(CreateTransactionPayment.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateTransactionPayment.CommandResult>> CreatePayment([FromRoute] Guid id)
        {
            var command = new CreateTransactionPayment.Command { TenantId = TenantId, TransactionId = id };
            return Ok(await Mediator.Send(command));
        }
    }
}