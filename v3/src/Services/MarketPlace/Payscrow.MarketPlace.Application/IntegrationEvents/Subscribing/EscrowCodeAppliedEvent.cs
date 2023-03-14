using MicroRabbit.Domain.Core.Bus;
using Microsoft.EntityFrameworkCore;
using Payscrow.Core.Events;
using Payscrow.MarketPlace.Application.Domain.Entities;
using Payscrow.MarketPlace.Application.Domain.Enumerations;
using Payscrow.MarketPlace.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.IntegrationEvents.Subscribing
{
    public class EscrowCodeAppliedEvent : IntegrationEvent
    {
        public EscrowCodeAppliedEvent(Guid tenantId, Guid transactionGuid)
            : base(tenantId)
        {
            TransactionGuid = transactionGuid;
        }

        public Guid TransactionGuid { get; }
    }

    public class EscrowCodeAppliedEventHandler : IEventHandler<EscrowCodeAppliedEvent>
    {
        private readonly IMarketPlaceDbContext _context;

        public EscrowCodeAppliedEventHandler(IMarketPlaceDbContext context)
        {
            _context = context;
        }

        public async Task Handle(EscrowCodeAppliedEvent @event)
        {
            var transaction = await _context.Transactions.ForTenant(@event.TenantId)
                        .FirstOrDefaultAsync(x => x.Id == @event.TransactionGuid);

            if (transaction is null) return;

            transaction.StatusId = TransactionStatus.Completed.Id;
            transaction.InEscrow = false;

            var statusLog = new TransactionStatusLog
            {
                TransactionId = transaction.Id,
                StatusId = transaction.StatusId,
                InDispute = transaction.InDispute,
                InEscrow = transaction.InEscrow,
                Note = "Escrow transaction code applied!"
            };
            _context.TransactionStatusLogs.Add(statusLog);

            await _context.SaveChangesAsync();
        }
    }
}