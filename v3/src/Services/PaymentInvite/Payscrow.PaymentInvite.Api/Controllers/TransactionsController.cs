using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payscrow.PaymentInvite.Application.Commands;
using Payscrow.PaymentInvite.Application.Queries;
using Payscrow.PaymentInvite.Application.Queries.Deals;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Api.Controllers
{
    public class TransactionsController : BaseApiController
    {
        [Authorize]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(GetAccountTransactions.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAccountTransactions.QueryResult>> GetListAsync([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string currencyCode, [FromBody] GetAccountTransactions.Query.SearchParameter search)
        {
            return Ok(await Mediator.Send(new GetAccountTransactions.Query { Page = page, PageSize = pageSize, CurrencyCode = currencyCode, Search = search }));
        }


        [Authorize]
        [HttpGet]
        [Route("seller/detail/{id}")]
        [ProducesResponseType(typeof(GetSellerAccountTransactionDetail.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetSellerAccountTransactionDetail.QueryResult>> GetSellerTransactionDetailAsync([FromRoute]Guid id)
        {
            return Ok(await Mediator.Send(new GetSellerAccountTransactionDetail.Query { TransactionId = id  }));
        }


        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(BuyerCreateTransaction.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BuyerCreateTransaction.CommandResult>> Create([FromBody] BuyerCreateTransaction.Command command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        [Route("payment-link")]
        [ProducesResponseType(typeof(GetPaymentLink.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPaymentLink.QueryResult>> GetPaymentLink([FromBody] GetPaymentLink.Query query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
