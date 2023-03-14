using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Linq;
using System;

namespace Payscrow.Payments.Api.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected Guid TenantId => HttpContext.Request.Headers["TenantId"].FirstOrDefault().ToGuid();
    }
}
