using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Payscrow.Escrow.Api.Infrastructure.Filters
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}