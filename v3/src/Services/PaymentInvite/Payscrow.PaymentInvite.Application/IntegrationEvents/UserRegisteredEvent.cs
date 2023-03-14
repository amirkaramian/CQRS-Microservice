using Payscrow.Core.Events;
using System;

namespace Payscrow.PaymentInvite.Application.IntegrationEvents
{
    public class UserRegisteredEvent : IntegrationEvent
    {
        public string UserId { get; }
        public string AccountId { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string PhoneNumber { get; }

        public UserRegisteredEvent(string userId, string accountId, string tenantId, string email, string firstName, string lastName, string phoneNumber)
         : base(tenantId.ToGuid())
        {
            UserId = userId;
            AccountId = accountId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }
    }
}