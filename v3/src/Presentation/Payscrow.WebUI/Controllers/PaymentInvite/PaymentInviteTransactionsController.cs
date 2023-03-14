using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payscrow.WebUI.Models.PaymentInvite;
using Payscrow.WebUI.Models.Payments;
using Payscrow.WebUI.Services;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Controllers.PaymentInvite
{
    public class PaymentInviteTransactionsController : BaseApiController
    {
        private readonly PaymentInviteService _paymentInviteService;

        public PaymentInviteTransactionsController(PaymentInviteService paymentInviteService)
        {
            _paymentInviteService = paymentInviteService;
        }


        [Route("create-transaction-data")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCreateTransactionDatasAsync(string dealId)
        {
            var response = await _paymentInviteService.GetDealDetailsModel(dealId);
            var dealDetailsResponse = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(dealDetailsResponse));
        }


        [Route("create-transaction")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateTransactionAsync([FromBody] CreateTransactionRequestModel model)
        {
            var response = await _paymentInviteService.CreateNewTransactionAsync(model);

            var createInviteResponse = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(createInviteResponse));
        }


        //[Route("payment-link")]
        //[HttpPost]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //public async Task<ActionResult> GetPaymentLinkAsync([FromBody] PaymentLinkRequestModel model)
        //{
        //    var response = await _paymentInviteService.GetPaymentLink(model);

        //    var paymentLinkResponse = await response.Content.ReadAsStringAsync();

        //    return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(paymentLinkResponse));
        //}

    }
}
