using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Payscrow.Payments.Api.Infrastructure.Filters
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}