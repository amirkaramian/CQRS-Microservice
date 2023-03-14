using MicroRabbit.Domain.Core.Bus;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Domain.Models;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.IntegrationEvents.Handlers
{
    public class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>, ISelfTransientLifetime
    {
        private readonly IEscrowDbContext _context;

        public UserRegisteredEventHandler(IEscrowDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UserRegisteredEvent integrationEvent)
        {
            var accountGuid = integrationEvent.AccountId.ToGuid();

            var account = new Account
            {
                Name = $"{integrationEvent.FirstName} {integrationEvent.LastName}",
                Email = integrationEvent.Email,
                AccountGuid = accountGuid,
                PhoneNumber = integrationEvent.PhoneNumber,
                TenantId = integrationEvent.TenantId
            };

            _context.Accounts.Add(account);

            var userGuid = integrationEvent.UserId.ToGuid();

            var user = new User
            {
                FirstName = integrationEvent.FirstName,
                LastName = integrationEvent.LastName,
                UserGuid = userGuid,
                PhoneNumber = integrationEvent.PhoneNumber,
                Email = integrationEvent.Email,
                TenantId = integrationEvent.TenantId
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }
    }
}