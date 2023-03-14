using MicroRabbit.Domain.Core.Bus;
using Microsoft.EntityFrameworkCore;
using Payscrow.Core.Bus;
using Payscrow.PaymentInvite.Application.Interfaces;
using Payscrow.PaymentInvite.Domain.Enumerations;
using System;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.IntegrationEvents.Handlers
{
    public class PaymentVerifiedEventHandler : IEventHandler<PaymentVerifiedIntegrationEvent>, IEventHandlerService
    {
        private readonly IPaymentInviteDbContext _context;
        private readonly IEventBus _eventBus;

        public PaymentVerifiedEventHandler(IPaymentInviteDbContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
        }

        public async Task Handle(PaymentVerifiedIntegrationEvent @event)
        {
            var transactionId = Guid.Parse(@event.TransactionId);
            var transaction = await _context.Transactions
                .Include(x => x.Deal)
                .ThenInclude(x => x.Seller)
                .Include(x => x.Deal)
                .ThenInclude(x => x.Currency)
                .SingleOrDefaultAsync(x => x.Id == transactionId);

            if (transaction != null)
            {
                if (transaction.PaymentStatus == PaymentStatus.Unpaid)
                {
                    transaction.PaymentStatus = PaymentStatus.Paid;
                    transaction.InEscrow = true;

                    await _context.SaveChangesAsync();

                    var escrowCharge = transaction.SellerChargeAmount + transaction.BuyerChargeAmount;

                    _eventBus.Publish(new TransactionEscrowedEvent(
                        transaction.Deal.Seller.AccountId,
                        transaction.TotalAmount,
                        escrowCharge,
                        transaction.Deal?.Currency?.Code,
                        @event.TenantId));
                }
            }
        }
    }
}