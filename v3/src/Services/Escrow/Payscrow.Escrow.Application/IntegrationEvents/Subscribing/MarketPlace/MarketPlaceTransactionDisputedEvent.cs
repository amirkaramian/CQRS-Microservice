using MicroRabbit.Domain.Core.Bus;
using Microsoft.EntityFrameworkCore;
using Payscrow.Core.Events;
using Payscrow.Escrow.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.IntegrationEvents.Subscribing.MarketPlace
{
    public class MarketPlaceTransactionDisputedEvent : IntegrationEvent
    {
        public Guid TransactionGuid { get; }
        public Guid MerchantAccountGuid { get; }
        public Guid CustomerAccountGuid { get; }
        public Guid BrokerAccountGuid { get; }
        public Guid DisputeRaisedByAccountId { get; }
        public string Complaint { get; }

        public MarketPlaceTransactionDisputedEvent(Guid tenantId, Guid transactionGuid,
            Guid merchantAccountGuid,
            Guid customerAccountGuid,
            Guid brokerAccountGuid,
            Guid disputeRaisedByAccountId,
            string complaint) : base(tenantId)
        {
            TransactionGuid = transactionGuid;
            MerchantAccountGuid = merchantAccountGuid;
            CustomerAccountGuid = customerAccountGuid;
            BrokerAccountGuid = brokerAccountGuid;
            DisputeRaisedByAccountId = disputeRaisedByAccountId;
            Complaint = complaint;
        }
    }

    public class MarketPlaceTransactionDisputedEventHandler : IEventHandler<MarketPlaceTransactionDisputedEvent>, ISelfTransientLifetime
    {
        private readonly IEscrowDbContext _context;

        public MarketPlaceTransactionDisputedEventHandler(IEscrowDbContext context)
        {
            _context = context;
        }

        public async Task Handle(MarketPlaceTransactionDisputedEvent @event)
        {
            var transaction = await _context.EscrowTransactions.ForTenant(@event.TenantId)
                .FirstOrDefaultAsync(x => x.TransactionGuid == @event.TransactionGuid);

            if (transaction is null) return;

            transaction.InDispute = true;
            transaction.UpdateUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}