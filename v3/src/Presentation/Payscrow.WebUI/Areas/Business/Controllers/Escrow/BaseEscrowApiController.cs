using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Payscrow.WebUI.Areas.Business.Controllers.Escrow
{
    [Route("api/v3/business/escrow/[controller]")]
    [ApiController, Authorize]
    public class BaseEscrowApiController : ControllerBase
    {
    }
}