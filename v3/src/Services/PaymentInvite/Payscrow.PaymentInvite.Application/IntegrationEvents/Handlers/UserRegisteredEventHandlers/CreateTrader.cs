using MicroRabbit.Domain.Core.Bus;
using Microsoft.EntityFrameworkCore;
using Payscrow.PaymentInvite.Application.Interfaces;
using Payscrow.PaymentInvite.Domain.Models;
using Payscrow.PaymentInvite.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.IntegrationEvents.Handlers.UserRegisteredEventHandlers
{
    public class CreateTrader : IEventHandler<UserRegisteredEvent>, IEventHandlerService
    {
        private readonly IPaymentInviteDbContext _context;

        public CreateTrader(IPaymentInviteDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UserRegisteredEvent @event)
        {
            var accountGuid = @event.AccountId.ToGuid();
            if (await _context.Traders.AnyAsync(x => x.AccountId == accountGuid)) return;

            var trader = new Trader
            {
                CreateUserId = @event.UserId.ToGuid(),
                AccountId = accountGuid,
                TenantId = @event.TenantId,
                EmailAddress = @event.Email,
                Name = $"{@event.FirstName} {@event.LastName}",
                PhoneNumber = new PhoneNumber("", @event.PhoneNumber)
            };

            _context.Traders.Add(trader);

            await _context.SaveChangesAsync();
        }
    }
}