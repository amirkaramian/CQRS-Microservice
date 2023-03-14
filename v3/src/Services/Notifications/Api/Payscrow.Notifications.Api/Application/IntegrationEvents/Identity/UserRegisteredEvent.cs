using MicroRabbit.Domain.Core.Bus;
using Payscrow.Core.Events;
using Payscrow.Notifications.Api.Application.Enumerations;
using Payscrow.Notifications.Api.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Application.IntegrationEvents.Identity
{
    public class UserRegisteredEvent : IntegrationEvent
    {
        public string UserId { get; }
        public string AccountId { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string PhoneNumber { get; }
        public bool IsSystemGeneratedUser { get; }
        public string Password { get; }

        public UserRegisteredEvent(string userId, string accountId, string tenantId, string email,
            string firstName, string lastName, string phoneNumber, bool isSystemGeneratedUser, string password)
            : base(tenantId.ToGuid())
        {
            UserId = userId;
            AccountId = accountId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            IsSystemGeneratedUser = isSystemGeneratedUser;
            Password = password;
        }
    }

    public class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>, IEventHandlerService
    {
        private readonly IEmailNotificationService _emailNotificationService;

        public UserRegisteredEventHandler(IEmailNotificationService emailNotificationService)
        {
            _emailNotificationService = emailNotificationService;
        }

        public async Task Handle(UserRegisteredEvent @event)
        {
            if (@event.IsSystemGeneratedUser)
            {
                await _emailNotificationService.SendAsync(@event.TenantId,
                                EmailMessageType.SystemGeneratedUser,
                                @event.Email, "Your New Account On PayScrow",
                                new Dictionary<string, object> {
                                    { "firstName", @event.FirstName },
                                    { "email", @event.Email },
                                    { "password", @event.Password }
                                });
            }
            else
            {
                await _emailNotificationService.SendAsync(@event.TenantId,
                                EmailMessageType.NewRegisteredUser,
                                @event.Email, "Welcome on board PayScrow",
                                new Dictionary<string, object> {
                                    { "firstName", @event.FirstName }
                                });
            }
        }
    }
}