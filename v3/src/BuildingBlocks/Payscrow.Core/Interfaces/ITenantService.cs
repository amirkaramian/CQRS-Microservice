using System;
using System.Threading.Tasks;

namespace Payscrow.Core.Interfaces
{
    public interface ITenantService
    {
        Task<Guid> GetTenantIdAsync();
    }
}
