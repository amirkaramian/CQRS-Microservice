using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Payscrow.WebUI.Areas.Business.Controllers.MarketPlace
{
    [Route("api/v3/business/marketplace/[controller]")]
    [ApiController, Authorize]
    public class BaseMarketPlaceApiController : ControllerBase
    {
    }
}