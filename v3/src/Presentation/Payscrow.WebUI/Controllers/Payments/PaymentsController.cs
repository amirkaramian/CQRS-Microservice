using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payscrow.WebUI.Models.Payments;
using Payscrow.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Controllers.Payments
{
    public class PaymentsController : BaseApiController
    {
        private readonly PaymentsService _paymentsService;

        public PaymentsController(PaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        [HttpGet("payment-methods")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetPaymentMethodsAsync([FromQuery] string currencyCode, [FromQuery] string accountId)
        {
            var response = await _paymentsService.GetPaymentMethods(currencyCode, accountId);
            var paymentMethodsResponse = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(paymentMethodsResponse));
        }

        [Route("payment-link")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetPaymentLinkAsync([FromBody] PaymentLinkRequestModel model)
        {
            var response = await _paymentsService.GetPaymentLink(model);

            var paymentLinkResponse = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(paymentLinkResponse));
        }

        [Route("verify-payment")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> VerifyPaymentAsync([FromBody] VerifyPaymentRequestModel model)
        {
            var response = await _paymentsService.VerifyPayment(model);

            var paymentVerificationResponse = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(paymentVerificationResponse));
        }
    }
}