using System;
using System.Threading.Tasks;

namespace Payscrow.Core.Interfaces
{
    public interface IIdentityService
    {
        Guid? UserIdentity { get; }
        Guid? AccountId { get; }
        Guid? TenantId { get; }
        string Email { get; }
        string Phone { get; }

        string UserName { get; }
        string AccountName { get; }

        bool IsAuthenticated { get; }

        bool IsInRole(string role);
    }
}