using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Payscrow.EscrowDirect.Application.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Payscrow.EscrowDirect.Api.Infrastructure.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "MerchantApiKey";


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key was not provided"
                };
                return;
            }

            var dbContext = context.HttpContext.RequestServices.GetService<IEscrowDirectDbContext>();

            var merchantId = await dbContext.Merchants.Where(x => x.ApiKey == extractedApiKey)
                .Select(x => x.Id).FirstOrDefaultAsync();

            if (merchantId != Guid.Empty)
            {
                context.HttpContext.Items.Add("merchant_id", merchantId.ToString());
            }

            await next();
        }
    }
}
