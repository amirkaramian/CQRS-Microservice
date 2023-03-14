using System.Threading.Tasks;

namespace Payscrow.Escrow.Application
{
    public interface IBackgroundJob
    {
        Task ExecuteAsync();
    }
}