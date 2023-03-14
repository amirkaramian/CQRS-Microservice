using Microsoft.AspNetCore.Mvc;
using Payscrow.Payments.Api.Application.Commands.Public;
using Payscrow.Payments.Api.Application.Queries.Public;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Controllers
{
    public class PaymentsController : BaseApiController
    {
        [HttpGet("methods")]
        [ProducesResponseType(typeof(GetAvailablePaymentMethods.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAvailablePaymentMethods.QueryResult>> GetPaymentMethodsAsync([FromQuery] string currencyCode, [FromQuery] string accountId)
        {
            return Ok(await Mediator.Send(new GetAvailablePaymentMethods.Query { CurrencyCode = currencyCode, AccountId = accountId.ToGuid(), TenantId = TenantId }));
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(CreatePayment.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatePayment.CommandResult>> CreatePaymentAsync([FromBody] CreatePayment.Command command)
        {
            command.TenantId = TenantId;
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        [Route("payment-link")]
        [ProducesResponseType(typeof(GetProviderPaymentLink.QueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProviderPaymentLink.QueryResult>> GetPaymentLink([FromBody] GetProviderPaymentLink.Query query)
        {
            query.TenantId = TenantId;
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [Route("verify")]
        [ProducesResponseType(typeof(VerifyPayment.CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<VerifyPayment.CommandResult>> VerifyPayment([FromBody] VerifyPayment.Command command)
        {
            command.TenantId = TenantId;
            return Ok(await Mediator.Send(command));
        }
    }
}