using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payscrow.PaymentInvite.Application.Commands;
using Payscrow.PaymentInvite.Application.Interfaces;
using Payscrow.PaymentInvite.Application.Queries;

namespace Payscrow.PaymentInvite.Api.Controllers
{
    [Authorize]
    public class DealsController : BaseApiController
    {
        [Authorize]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(GetAccountDeals.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAccountDeals.QueryResult>> GetDealsAsync([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string currencyCode, [FromBody] GetAccountDeals.Query.SearchParameter search)
        {
            return Ok(await Mediator.Send(new GetAccountDeals.Query { Page = page, PageSize = pageSize, CurrencyCode = currencyCode, Search = search }));
        }


        [HttpGet]
        [Route("details")]
        [ProducesResponseType(typeof(GetDealDetails.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetDealDetails.QueryResult>> GetDetailsAsync(Guid? dealId)
        {
            return Ok(await Mediator.Send(new GetDealDetails.Query { DealId = dealId }));
        }


        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(SellerCreateDeal.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SellerCreateDeal.CommandResult>> Create([FromBody] SellerCreateDeal.Command command)
        {
            return Ok(await Mediator.Send(command));
        }


        [HttpPost]
        [Route("verify")]
        [ProducesResponseType(typeof(SellerConfirmAnonymousDeal.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SellerConfirmAnonymousDeal.CommandResult>> Verify([FromBody] SellerConfirmAnonymousDeal.Command command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
