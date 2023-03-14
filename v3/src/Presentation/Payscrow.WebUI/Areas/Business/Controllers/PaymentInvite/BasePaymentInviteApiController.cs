using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Payscrow.WebUI.Areas.Business.Controllers.PaymentInvite
{
    [Route("api/v3/business/paymentinvite/[controller]")]
    [ApiController, Authorize]
    public class BasePaymentInviteApiController : ControllerBase
    {
    }
}