using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Payscrow.Core.Interfaces;
using Payscrow.Infrastructure.Common;
using Payscrow.Infrastructure.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Services
{
    public class WebTenantService : TenantService, ITenantService
    {
        public WebTenantService(ConfigSettings configSettings, ILogger<TenantService> logger, IHttpContextAccessor httpContextAccessor)
            : base(configSettings, logger, httpContextAccessor)
        {
        }

        //public new async Task<Guid> GetTenantIdAsync()
        //{
        //    var result = Guid.Empty;

        //    if (_tenantId.HasValue)
        //    {
        //        return _tenantId.Value;
        //    }

        //    try
        //    {
        //        var monoRequest = new RestRequest($"api/v3/tenants/id/{GetHostName()}", Method.GET);
        //        monoRequest.AddHeader("Accept", "application/json");
        //        monoRequest.AddHeader("Content-Type", "application/json");

        //        var response = await _restClient.ExecuteAsync<Guid>(monoRequest);

        //        if (response.IsSuccessful)
        //        {
        //            result = response.Data;
        //            if (result == Guid.Empty)
        //            {
        //                _logger.LogWarning("an invalid host tried to access the system with the hostname: {HostName}", result);
        //            }

        //            _tenantId = result;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e, "An error occured while getting Tenant ID from Identity service");
        //    }

        //    return result;
        //}

        //private string GetHostName()
        //{
        //    return _httpContextAccessor?.HttpContext?.Request?.Host.Value?.Trim();
        //}
    }
}