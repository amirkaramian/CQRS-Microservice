using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Api.Infrastructure.Filters
{
    public class TenantFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("TenantId", out var extractedTenantId))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Tenant id not provided"
                };
                return;
            }

            await next();
        }
    }
}