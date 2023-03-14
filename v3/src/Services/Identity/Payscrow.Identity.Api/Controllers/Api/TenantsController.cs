using Microsoft.AspNetCore.Mvc;
using Payscrow.Identity.Api.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.Identity.Api.Controllers.Api
{
    public class TenantsController : BaseApiController
    {
        private readonly ITenentProvider _tenantProvider;

        public TenantsController(ITenentProvider tenantProvider)
        {
            _tenantProvider = tenantProvider;
        }

        [HttpGet("id/{hostname}")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> GetTenantIdAsync(string hostname)
        {
            return Ok(await _tenantProvider.GetTenantIdAsync(hostname));
        }
    }
}
