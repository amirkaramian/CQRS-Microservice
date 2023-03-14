using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payscrow.WebUI.Areas.Business.Common;
using Payscrow.WebUI.Areas.Business.Dtos.PaymentInvite;
using Payscrow.WebUI.Areas.Business.Services.PaymentInvite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Areas.Business.Controllers.PaymentInvite
{
    public class TransactionsController : BasePaymentInviteApiController
    {
        private readonly TransactionBusinessService _transactionBusinessService;

        public TransactionsController(TransactionBusinessService transactionService)
        {
            _transactionBusinessService = transactionService;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(BaseDataTableResult<TransactionDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDealsAsync([FromForm] DataTableRequest request, [FromQuery] string currencyCode)
        {
            return Ok(await _transactionBusinessService.GetTransactionsAsync(request, currencyCode));
        }

        [Route("seller/detail")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetSellerTransactionDetail([FromQuery] string transactionId)
        {
            var response = await _transactionBusinessService.GetSellerTransactionDetail(transactionId);
            var responseString = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(responseString));
        }
    }
}