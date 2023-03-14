using MicroRabbit.Domain.Core.Bus;
using Microsoft.EntityFrameworkCore;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Domain.Enumerations;
using Payscrow.Escrow.Domain.Models;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.IntegrationEvents.Handlers
{
    public class TransactionEscrowedEventHandler : IEventHandler<TransactionEscrowedEvent>, ISelfTransientLifetime
    {
        private readonly IEscrowDbContext _context;

        public TransactionEscrowedEventHandler(IEscrowDbContext context)
        {
            _context = context;
        }

        async public Task Handle(TransactionEscrowedEvent @event)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountGuid == @event.AccountGuid);

            if(account != null)
            {
                var currency = await _context.Currencies.FirstOrDefaultAsync(x => x.Code == @event.CurrencyCode);

                var escrowTransaction = new Transaction(TransactionType.Credit,
                                                        @event.AmountPaid,
                                                        TransactionLocation.Escrow, "",
                                                        account.Id, currency.Id);

                var debitChargeTransaction = new Transaction(TransactionType.Debit,
                                                        @event.EscrowCharge, TransactionLocation.Escrow, "",
                                                        account.Id, currency.Id);

                _context.Transactions.Add(escrowTransaction);
                _context.Transactions.Add(debitChargeTransaction);

                await _context.SaveChangesAsync();
            }
        }
    }
}
