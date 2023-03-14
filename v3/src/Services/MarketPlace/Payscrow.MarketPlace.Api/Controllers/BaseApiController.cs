using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Payscrow.Core.Interfaces;
using System;
using System.Linq;

namespace Payscrow.MarketPlace.Api.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public Guid AccountId => HttpContext.RequestServices.GetService<IIdentityService>().AccountId ?? Guid.Empty;
        protected Guid TenantId => HttpContext.Request.Headers["TenantId"].FirstOrDefault().ToGuid();
    }
}