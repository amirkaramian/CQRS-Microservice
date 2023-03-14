using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Payscrow.Core.Interfaces;
using Payscrow.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Infrastructure
{
    public class HttpClientAuthorizationDelegatingHandler
        : DelegatingHandler, ITransientLifetime
    {
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly WebWorkContext _webWorkContext;
        private readonly ILogger _logger;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccesor, WebWorkContext webWorkContext, ILogger<HttpClientAuthorizationDelegatingHandler> logger)
        {
            _httpContextAccesor = httpContextAccesor;
            _webWorkContext = webWorkContext;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _httpContextAccesor.HttpContext.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }

            var tenantId = await _webWorkContext.GetTenantIdAsync();

            if (tenantId != Guid.Empty)
            {
                request.Headers.Add("TenantId", tenantId.ToString());
            }
            else
            {
                _logger.LogWarning("TenantId could not be resolved");
            }

            //if (request.Headers.TryGetValues("BrokerApiKey", out var values))
            //{
            //    request.Headers.Add("BrokerApiKey", values.FirstOrDefault());
            //}

            var token = await GetToken();

            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> GetToken()
        {
            const string ACCESS_TOKEN = "access_token";

            return await _httpContextAccesor.HttpContext.GetTokenAsync(ACCESS_TOKEN);
        }
    }
}