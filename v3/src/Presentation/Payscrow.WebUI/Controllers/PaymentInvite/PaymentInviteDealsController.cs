using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payscrow.WebUI.Models.PaymentInvite;
using Payscrow.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Controllers.PaymentInvite
{
    public class PaymentInviteDealsController : BaseApiController
    {
        private readonly PaymentInviteService _paymentInviteService;

        public PaymentInviteDealsController(PaymentInviteService paymentInviteService)
        {
            _paymentInviteService = paymentInviteService;
        }

        [Route("create-deal-data")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCreateDataAsync()
        {
            var data = await _paymentInviteService.GetCreateDealDataModel();
            data.BuyerPageUrl = "http://localhost:7300/paymentinvite/createtransaction";

            return Ok(data);
        }






        [Route("create-deal")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateDealAsync([FromBody] CreateDealRequestModel model)
        {
            var response = await _paymentInviteService.CreateNewDealAsync(model);

            var createInviteResponse = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(createInviteResponse));
        }


        [Route("verify-deal")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> VerifyAsync([FromBody] VerifyDealRequestModel model)
        {
            var response = await _paymentInviteService.VerifyAnonymousDealAsync(model);
            var createInviteResponse = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(createInviteResponse));
        }





    }
}
