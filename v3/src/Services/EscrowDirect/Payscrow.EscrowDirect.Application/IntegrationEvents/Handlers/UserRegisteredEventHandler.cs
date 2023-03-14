using MicroRabbit.Domain.Core.Bus;
using Microsoft.EntityFrameworkCore;
using Payscrow.EscrowDirect.Application.Domain.Entities;
using Payscrow.EscrowDirect.Application.Interfaces;
using Payscrow.EscrowDirect.Application.Interfaces.Markers;
using System.Threading.Tasks;

namespace Payscrow.EscrowDirect.Application.IntegrationEvents.Handlers
{
    public class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>, ISelfTransientLifetime
    {
        private readonly IEscrowDirectDbContext _context;

        public UserRegisteredEventHandler(IEscrowDirectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UserRegisteredEvent @event)
        {
            var accountGuid = @event.AccountId.ToGuid();
            if (await _context.Merchants.AnyAsync(x => x.AccountId == accountGuid)) return;

            var merchant = new Merchant
            {
                CreateUserId = @event.UserId.ToGuid(),
                AccountId = accountGuid,
                TenantId = @event.TenantId,
                EmailAddress = @event.Email,
                Name = $"{@event.FirstName} {@event.LastName}",
                PhoneNumber = @event.PhoneNumber,
                ApiKey = RandomHelper.RandomString()
            };

            _context.Merchants.Add(merchant);

            await _context.SaveChangesAsync();
        }
    }
}