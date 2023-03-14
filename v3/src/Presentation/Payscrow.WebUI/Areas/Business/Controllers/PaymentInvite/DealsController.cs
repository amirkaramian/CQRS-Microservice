using Microsoft.AspNetCore.Mvc;
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
    public class DealsController : BasePaymentInviteApiController
    {
        private readonly DealBusinessService _dealsService;

        public DealsController(DealBusinessService dealsService)
        {
            _dealsService = dealsService;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(BaseDataTableResult<DealDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDealsAsync([FromForm] DataTableRequest request, [FromQuery] string currencyCode)
        {
            return Ok(await _dealsService.GetDealsAsync(request, currencyCode));
        }
    }
}