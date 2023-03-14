using Microsoft.AspNetCore.Mvc;
using Payscrow.Identity.Api.Dtos;
using Payscrow.Identity.Api.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.Identity.Api.Controllers.Api
{
    public class AccountsController : BaseApiController
    {
        private readonly AccountService _accountService;

        public AccountsController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("brief/{apikey}/{tenantId}")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AccountDto>> GetAccountBriefAsync(string apiKey, Guid tenantId)
        {
            return Ok(await _accountService.GetAccountByApiKeyAsync(apiKey, tenantId));
        }

        [HttpGet("get-create/{email}/{tenantId}")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AccountDto>> GetOrCreateAsync(string email, Guid tenantId)
        {
            return Ok(await _accountService.GetOrCreateAccountWithoutPassword(email, tenantId));
        }

        [HttpPost("quick-create")]
        [ProducesResponseType(typeof(AccountDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AccountDto>> QuickCreate([FromBody] QuickCreateAccountDto dto)
        {
            return Ok(await _accountService.QuickCreateOrGetAccountWithGeneratedPassword(dto));
        }
    }
}