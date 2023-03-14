using MicroRabbit.Domain.Core.Bus;
using Microsoft.EntityFrameworkCore;
using Payscrow.Core.Bus;
using Payscrow.Core.Events;
using Payscrow.MarketPlace.Application.Data;
using Payscrow.MarketPlace.Application.Domain.Entities;
using Payscrow.MarketPlace.Application.Domain.Enumerations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.IntegrationEvents
{
    public class PaymentVerifiedIntegrationEvent : IntegrationEvent
    {
        public PaymentVerifiedIntegrationEvent(string transactionId, decimal amount, string name, string email, DateTime paymentDate, Guid tenantId)
            : base(tenantId)
        {
            TransactionId = transactionId;
            Amount = amount;
            Name = name;
            Email = email;
            PaymentDate = paymentDate;
        }

        public string TransactionId { get; }
        public decimal Amount { get; }
        public string Name { get; }
        public string Email { get; }
        public DateTime PaymentDate { get; }
    }

    public class PaymentVerifiedIntegrationEventHandler : IEventHandler<PaymentVerifiedIntegrationEvent>
    {
        private readonly MarketPlaceDbContext _context;
        private readonly IEventBus _eventBus;

        public PaymentVerifiedIntegrationEventHandler(MarketPlaceDbContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
        }

        public async Task Handle(PaymentVerifiedIntegrationEvent @event)
        {
            var transactionId = @event.TransactionId.ToGuid();

            var transaction = await _context.Transactions.Include(x => x.Currency).FirstOrDefaultAsync(x => x.Id == transactionId);

            if (transaction == null) return;

            transaction.StatusId = TransactionStatus.InProgress.Id;
            transaction.PaymentStatus = TransactionPaymentStatus.Paid;
            transaction.InEscrow = true;
            transaction.EscrowCode = RandomHelper.GenerateRandomCode(6).ToUpper();

            var statusLog = new TransactionStatusLog
            {
                TransactionId = transaction.Id,
                StatusId = transaction.StatusId,
                InDispute = transaction.InDispute,
                InEscrow = transaction.InEscrow,
                Note = "Payment made and verified"
            };
            _context.TransactionStatusLogs.Add(statusLog);

            var payment = new Payment
            {
                Amount = @event.Amount,
                Type = PaymentType.Credit,
                TransactionId = transactionId,
                TenantId = transaction.TenantId,
                PaymentDate = @event.PaymentDate
            };
            _context.Payments.Add(payment);

            await _context.SaveChangesAsync();

            var settlementAccounts = await _context.SettlementAccounts.ForTenant(transaction.TenantId)
                .Where(x => x.TransactionId == transaction.Id).Select(x => new MarketPlacePaymentVerifiedEvent.Settlement
                {
                    AccountName = x.AccountName,
                    AccountNumber = x.AccountNumber,
                    Amount = x.Amount,
                    BankCode = x.BankCode
                })
                .ToListAsync();

            _eventBus.Publish(
                new MarketPlacePaymentVerifiedEvent(
                    transaction.TenantId,
                    transaction.Id,
                    transaction.Number,
                    transaction.EscrowCode,
                    transaction.CustomerAccountId.Value,
                    transaction.CustomerEmailAddress,
                    transaction.CustomerPhone,
                    transaction.MerchantAccountId,
                    transaction.MerchantEmailAddress,
                    transaction.MerchantPhone,
                    transaction.BrokerAccountId,
                    payment.Amount,
                    transaction.Currency?.Code,
                    settlementAccounts));
        }
    }
}