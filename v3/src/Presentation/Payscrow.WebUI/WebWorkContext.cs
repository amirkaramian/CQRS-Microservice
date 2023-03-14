using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Payscrow.Core.Interfaces;
using Payscrow.WebUI.Models.Common;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Payscrow.WebUI
{
    public class WebWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityService _identityService;
        private readonly RestClient _restClient;
        private readonly ILogger _logger;

        public WebWorkContext(IIdentityService identityService, IHttpContextAccessor httpContextAccessor, ILogger<WebWorkContext> logger, IConfiguration configuration)
        {
            _identityService = identityService;
            _httpContextAccessor = httpContextAccessor;
            _restClient = new RestClient(configuration.GetValue<string>("IdentityUrl"));
            _logger = logger;
        }

        //private CurrencyModel _cachedCurrency;
        private Guid? _tenantId;

        //public async Task<CurrencyModel> GetWorkingCurrencyAsync()
        //{
        //    //whether there is a cached value
        //    if (_cachedCurrency != null)
        //        return _cachedCurrency;
        //}

        public async Task<Guid> GetTenantIdAsync()
        {
            var result = Guid.Empty;

            if (_tenantId.HasValue)
            {
                return _tenantId.Value;
            }

            if (_identityService.TenantId.HasValue)
            {
                return _identityService.TenantId.Value;
            }

            try
            {
                var request = new RestRequest($"api/v3/tenants/id/{GetHostName()}", Method.GET);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");

                var response = await _restClient.ExecuteAsync<Guid>(request);

                if (response.IsSuccessful)
                {
                    result = response.Data;
                    if (result == Guid.Empty)
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
            //var tHostName = "http://sandbox.payscrow.net/";

            //string[] hostParts = _httpContextAccessor?.HttpContext?.Request?.Host.Host?.Split('.');
            ////string[] hostParts = tHostName.Split('.');
            //return string.Join(".", hostParts?.Skip(Math.Max(0, hostParts.Length - 2))?.Take(2));

            var host = _httpContextAccessor?.HttpContext?.Request?.Host.Value;
            var domain = host.Substring(host.LastIndexOf('.', host.LastIndexOf('.') - 1) + 1);

            return domain;
        }
    }
}