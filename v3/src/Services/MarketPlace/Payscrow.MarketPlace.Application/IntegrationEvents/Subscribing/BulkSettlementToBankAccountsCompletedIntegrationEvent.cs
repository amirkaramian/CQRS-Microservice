using MicroRabbit.Domain.Core.Bus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payscrow.Core.Events;
using Payscrow.MarketPlace.Application.Domain.Entities;
using Payscrow.MarketPlace.Application.Domain.Enumerations;
using Payscrow.MarketPlace.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.IntegrationEvents.Subscribing
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
        : IEventHandler<BulkSettlementToBankAccountsCompletedIntegrationEvent>
    {
        private readonly IMarketPlaceDbContext _context;
        private readonly ILogger _logger;

        public BulkSettlementToBankAccountsCompletedIntegrationEventHandler(IMarketPlaceDbContext context,
            ILogger<BulkSettlementToBankAccountsCompletedIntegrationEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(BulkSettlementToBankAccountsCompletedIntegrationEvent @event)
        {
            var transaction = await _context.Transactions.ForTenant(@event.TenantId)
                .SingleOrDefaultAsync(x => x.Id == @event.TransactionGuid);

            if (transaction is null) return;

            transaction.StatusId = TransactionStatus.Finalized.Id;

            var statusLog = new TransactionStatusLog
            {
                TransactionId = transaction.Id,
                StatusId = transaction.StatusId,
                InDispute = transaction.InDispute,
                InEscrow = transaction.InEscrow,
                Note = "Escrow transaction Finalized!"
            };
            _context.TransactionStatusLogs.Add(statusLog);

            await _context.SaveChangesAsync();

            _logger.LogInformation("---- Marked the Market place Transaction with GUID: {0} as Finalized ----", transaction.Id);
        }
    }
}