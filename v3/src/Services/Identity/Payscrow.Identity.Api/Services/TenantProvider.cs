using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payscrow.Identity.Api.Data;
using Payscrow.Identity.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Identity.Api.Services
{
    public class TenantProvider : ITenentProvider
    {
        private readonly ApplicationIdentityDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public TenantProvider(ApplicationIdentityDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<TenantProvider> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<Guid> GetTenantIdAsync()
        {
            var hostName = GetHostName()?.ToLower();

            var tenantId = await (from h in _context.Hosts
                                  join t in _context.Tenants
                                  on h.TenantId equals t.Id
                                  where h.Name == hostName
                                  select t.Id).SingleOrDefaultAsync();

            if (tenantId == default || tenantId == Guid.Empty)
            {
                throw new Exception($"TenantId not found from host name: {hostName}");
            }
            else
            {
                return tenantId;
            }
        }

        public async Task<Tenant> GetTenantAsync()
        {
            var hostName = GetHostName()?.ToLower();

            var tenant = await (from h in _context.Hosts
                                join t in _context.Tenants
                                on h.TenantId equals t.Id
                                where h.Name == hostName
                                select t).AsNoTracking().SingleOrDefaultAsync();

            if (tenant == null)
            {
                var ex = new Exception($"Tenant not found from host name: {hostName}");
                _logger.LogCritical("Issues with Tenant Identification", ex);
                throw ex;
            }

            return tenant;
        }

        public async Task<TenantPreference> GetTenantPreferenceAsync()
        {
            var tenant = await GetTenantAsync();

            return new TenantPreference
            {
                LogoUrl = tenant?.LogoUrl,
                Colour = tenant?.Colour
            };
        }

        private string GetHostName()
        {
            //string[] hostParts = _httpContextAccessor?.HttpContext?.Request?.Host.Host?.Split('.');
            //return string.Join(".", hostParts?.Skip(Math.Max(0, hostParts.Length - 2))?.Take(2));

            _logger.LogInformation($"Accessing Host: {_httpContextAccessor?.HttpContext?.Request?.Host} on value: {_httpContextAccessor?.HttpContext?.Request?.Host.Value}");

            var host = _httpContextAccessor?.HttpContext?.Request?.Host.Value;
            var domain = host.Substring(host.LastIndexOf('.', host.LastIndexOf('.') - 1) + 1);

            _logger.LogInformation($"Accessing host: {domain} for tenant.");

            return domain;
        }

        public async Task<Guid> GetTenantIdAsync(string hostName)
        {
            var tenantId = await (from h in _context.Hosts
                                  join t in _context.Tenants
                                  on h.TenantId equals t.Id
                                  where h.Name == hostName
                                  select t.Id).SingleOrDefaultAsync();

            return tenantId;
        }
    }
}