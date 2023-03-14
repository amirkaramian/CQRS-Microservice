using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Payscrow.Core.Interfaces;
using Newtonsoft.Json;

namespace Payscrow.MarketPlace.Api.Infrastructure.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "BrokerApiKey";
        private const string TENANTID = "TenantId";


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

            if(!context.HttpContext.Request.Headers.TryGetValue(TENANTID, out var tenantId))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "TenantId was not provided"
                };
                return;
            }

            var settings = context.HttpContext.RequestServices.GetService<AppSettings>();
            var httpClient = context.HttpContext.RequestServices.GetService<IHttpClientFactory>().CreateClient();

            var url = $"{settings.IdentityUrl}/api/v3/accounts/brief/{extractedApiKey}/{tenantId}";

            var response = await httpClient.GetAsync(url);

            var responseString = await response.Content.ReadAsStringAsync();

            var account = JsonConvert.DeserializeObject<AccountBriefResult>(responseString);

            if(account == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Invalid Api key"
                };
                return;
            }

            context.HttpContext.Items.Add("broker_account_id", account.Id);
            context.HttpContext.Items.Add("broker_name", account.Name);

            await next();
        }
    }

    public class AccountBriefResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
