using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payscrow.Escrow.Application.Commands.Settings;
using Payscrow.Escrow.Application.Queries;
using Payscrow.Escrow.Application.Queries.Business;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Api.Controllers
{
    [Authorize]
    public class AccountSettingController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(GetAccountSettings.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAccountSettings.QueryResult>> GetAsync()
        {
            var query = new GetAccountSettings.Query { AccountGuid = AccountId, TenantId = TenantId };
            return Ok(await Mediator.Send(query));
        }

        //[HttpGet("default-currency")]
        //[ProducesResponseType(typeof(GetAccountDefaultCurrency.QueryResult), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<GetAccountDefaultCurrency.QueryResult>> GetAccountCurrencyAsync()
        //{
        //    return Ok(await Mediator.Send(new GetAccountDefaultCurrency.Query()));
        //}

        [HttpGet]
        [Route("change-currency")]
        [ProducesResponseType(typeof(ChangeAccountDefaultCurrency.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ChangeAccountDefaultCurrency.CommandResult>> ChangeCurrencyAsync(string currencyCode)
        {
            return Ok(await Mediator.Send(new ChangeAccountDefaultCurrency.Command { CurrencyCode = currencyCode, TenantId = TenantId, AccountId = AccountId }));
        }
    }
}