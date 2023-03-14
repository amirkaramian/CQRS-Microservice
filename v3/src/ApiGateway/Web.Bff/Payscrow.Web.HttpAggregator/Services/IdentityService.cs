using Microsoft.AspNetCore.Http;
using System;

namespace Payscrow.Web.HttpAggregator.Services
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public string GetUserEmail()
        {
            return _context.HttpContext.User?.Identity?.Name;
        }

        public string GetUserIdentity()
        {
            return _context.HttpContext.User?.FindFirst("sub")?.Value;
        }

        public string GetUserPhone()
        {
            return _context.HttpContext.User?.FindFirst("phone")?.Value;
        }
    }
}
