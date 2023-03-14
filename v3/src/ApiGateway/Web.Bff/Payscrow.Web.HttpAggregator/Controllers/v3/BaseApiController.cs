using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Web.HttpAggregator.Controllers.v3
{
    [ApiController]
    [ApiVersion("3")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}
