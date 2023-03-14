using Microsoft.AspNetCore.Http;
using Payscrow.Core.Interfaces;
using System;
using System.Linq;

namespace Payscrow.Infrastructure.Common.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            UserIdentity = httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value?.ToGuid();
            AccountId = httpContextAccessor.HttpContext?.User?.FindFirst("account_id")?.Value?.ToGuid();
            AccountName = httpContextAccessor.HttpContext?.User?.FindFirst("account_name")?.Value;
            TenantId = httpContextAccessor.HttpContext?.User?.FindFirst("tenant_id")?.Value?.ToGuid();
            UserName = httpContextAccessor.HttpContext?.User?.Identity?.Name;

            Email = httpContextAccessor.HttpContext?.User?.FindFirst("email")?.Value;
            Phone = httpContextAccessor.HttpContext?.User?.FindFirst("phone")?.Value;

            IsAuthenticated = httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }

        public Guid? UserIdentity { get; }
        public Guid? AccountId { get; }
        public string Email { get; }
        public string UserName { get; }
        public bool IsAuthenticated { get; }
        public Guid? TenantId { get; }
        public string AccountName { get; }
        public string Phone { get; }

        public bool IsInRole(string role)
        {
            if (!IsAuthenticated) return false;
            return _httpContextAccessor.HttpContext.User.Claims.Any(x => x.Type == "role" && x.Value == role);
        }
    }
}