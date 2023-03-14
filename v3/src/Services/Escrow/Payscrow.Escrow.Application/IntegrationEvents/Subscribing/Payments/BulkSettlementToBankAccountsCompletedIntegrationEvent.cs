using MicroRabbit.Domain.Core.Bus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payscrow.Core.Events;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Domain.Enumerations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.IntegrationEvents.Subscribing.Payments
{
    public class BulkSettlementToBankAccountsCompletedIntegrationEvent : IntegrationEvent
    {
        public BulkSettlementToBankAccountsCompletedIntegrationEvent(Guid tenantId, Guid transactionGuid) : base(tenantId)
        {
            TransactionGuid = transactionGuid;
        }

        public Guid TransactionGuid { get; }
    }

    public class BulkSettlementToBankAccountsCompletedIntegrationEventHandler
        : IEventHandler<BulkSettlementToBankAccountsCompletedIntegrationEvent>, ISelfTransientLifetime
    {
        private readonly IEscrowDbContext _context;
        private readonly ILogger _logger;

        public BulkSettlementToBankAccountsCompletedIntegrationEventHandler(IEscrowDbContext context,
            ILogger<BulkSettlementToBankAccountsCompletedIntegrationEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(BulkSettlementToBankAccountsCompletedIntegrationEvent @event)
        {
            var escrowTransaction = await _context.EscrowTransactions.ForTenant(@event.TenantId)
                .SingleOrDefaultAsync(x => x.TransactionGuid == @event.TransactionGuid);

            if (escrowTransaction is null)
            {
                _logger.LogCritical("---- Could not find the escrow transction with the GUID: {0} ----", @event.TransactionGuid);
                return;
            }

            escrowTransaction.StatusId = EscrowTransactionStatus.CompletedSettlement.Id;

            var settlements = await _context.Settlements.ForTenant(@event.TenantId)
                                            .Where(x => x.EscrowTransactionId == escrowTransaction.Id)
                                            .ToListAsync();

            foreach (var settlement in settlements)
            {
                settlement.MarkAsSettled();
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("---- Marked the Escrow Transactio with GUID: {0} as Completed ----", escrowTransaction.TransactionGuid);
        }
    }
}