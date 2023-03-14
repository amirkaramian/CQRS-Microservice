using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Payscrow.Web.HttpAggregator.Models.PaymentInvite;
using Payscrow.Web.HttpAggregator.Services;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.Web.HttpAggregator.Controllers.v3
{
    public class PaymentInviteController : BaseApiController
    {
        private readonly ILogger<PaymentInviteController> _logger;
        private readonly IPaymentInviteService _paymentInviteService;

        public PaymentInviteController(ILogger<PaymentInviteController> logger, IPaymentInviteService paymentInviteService)
        {
            _logger = logger;
            _paymentInviteService = paymentInviteService;
        }

        [Route("init-data")]
        [ProducesResponseType(typeof(CreateDealViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateDealViewModel>> GetCreateDataAsync()
        {
            var model = await _paymentInviteService.GetCreateDealDataModel();
            return Ok(model);
        }


        [Route("create-deal")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateInviteResponseModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateDealRequestModel model)
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
