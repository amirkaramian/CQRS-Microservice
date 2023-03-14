using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Application.BackgroundJobs
{
    public interface IBackgroundJob
    {
        Task ExecuteAsync();
    }
}