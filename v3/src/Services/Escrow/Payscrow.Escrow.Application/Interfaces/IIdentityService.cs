using System;

namespace Payscrow.Escrow.Application.Interfaces
{
    public interface IIdentityService
    {
        Guid? UserIdentity { get; }
        Guid? AccountId { get; }
        string Email { get; }

        string UserName { get; }

        bool IsAuthenticated { get; }
    }
}
