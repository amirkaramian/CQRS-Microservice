using Payscrow.Core.Interfaces;
using RestSharp;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Payscrow.Infrastructure.Common.Services
{
    public class TenantService : ITenantService
    {
        private readonly ConfigSettings _configSettings;
        private readonly RestClient _restClient;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Guid? _tenantId;

        public TenantService(ConfigSettings configSettings, ILogger<TenantService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _configSettings = configSettings;
            _restClient = new RestClient(_configSettings.IdentityUrl);
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> GetTenantIdAsync()
        {
            var result = Guid.Empty;

            if (_tenantId.HasValue)
            {
                return _tenantId.Value;
            }

            try
            {
                var monoRequest = new RestRequest($"api/v3/tenants/id/{GetHostName()}", Method.GET);
                monoRequest.AddHeader("Accept", "application/json");
                monoRequest.AddHeader("Content-Type", "application/json");

                var response = await _restClient.ExecuteAsync<Guid>(monoRequest);

                if (response.IsSuccessful)
                {
                    result = response.Data;
                    if(result == Guid.Empty)
                    {
                        _logger.LogWarning("an invalid host tried to access the system with the hostname: {HostName}", result);
                    }
                 
                    _tenantId = result;                    
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while getting Tenant ID from Identity service");
            }

            return result;
        }


        private string GetHostName()
        {
            return _httpContextAccessor?.HttpContext?.Request?.Host.Value?.Trim();
        }
    }
}
