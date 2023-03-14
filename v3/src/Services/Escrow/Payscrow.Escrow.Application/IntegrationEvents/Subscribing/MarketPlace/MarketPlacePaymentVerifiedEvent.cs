using MicroRabbit.Domain.Core.Bus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payscrow.Core.Events;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Domain.Enumerations;
using Payscrow.Escrow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.IntegrationEvents.Subscribing.MarketPlace
{
    public class MarketPlacePaymentVerifiedEvent : IntegrationEvent
    {
        public MarketPlacePaymentVerifiedEvent(Guid tenantId, Guid transactionGuid, string transactionNumber, string escrowCode, Guid customerAccountId, string customerEmailAddress,
            string customerPhone, Guid merchantAccountId, string merchantEmailAddress, string merchantPhone, Guid brokerAccountId, decimal amountPaid, string currencyCode,
            List<Settlement> settlements)
            : base(tenantId)
        {
            TransactionGuid = transactionGuid;
            EscrowCode = escrowCode;
            TransactionNumber = transactionNumber;
            CustomerAccountId = customerAccountId;
            CustomerEmailAddress = customerEmailAddress;
            CustomerPhone = customerPhone;
            MerchantAccountId = merchantAccountId;
            MerchantEmailAddress = merchantEmailAddress;
            MerchantPhone = merchantPhone;
            BrokerAccountId = brokerAccountId;
            AmountPaid = amountPaid;
            CurrencyCode = currencyCode;
            Settlements = settlements;
        }

        public Guid TransactionGuid { get; }
        public string TransactionNumber { get; }
        public string EscrowCode { get; }
        public Guid CustomerAccountId { get; }
        public string CustomerEmailAddress { get; }
        public string CustomerPhone { get; }
        public Guid MerchantAccountId { get; }
        public string MerchantEmailAddress { get; }
        public string MerchantPhone { get; }
        public Guid BrokerAccountId { get; }

        public decimal AmountPaid { get; }
        public string CurrencyCode { get; }

        public List<Settlement> Settlements { get; }

        public class Settlement
        {
            public string BankCode { get; set; }
            public string AccountNumber { get; set; }
            public string AccountName { get; set; }
            public decimal Amount { get; set; }
        }
    }

    public class MarketPlacePaymentVerifiedEventHandler : IEventHandler<MarketPlacePaymentVerifiedEvent>, ISelfTransientLifetime
    {
        private readonly IEscrowDbContext _context;
        private readonly ILogger _logger;

        public MarketPlacePaymentVerifiedEventHandler(IEscrowDbContext context, ILogger<MarketPlacePaymentVerifiedEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(MarketPlacePaymentVerifiedEvent message)
        {
            var currency = await _context.Currencies.FirstOrDefaultAsync(x => x.Code == message.CurrencyCode && x.TenantId == message.TenantId);

            if (currency is null)
            {
                _logger.LogCritical("----- Currency of transaction paid from Market place could not be found with Currency Code: {CurrencyCode} and TenantID: {TenantId} For Transaction ID: {TransactionGuid} -----",
                    message.CurrencyCode, message.TenantId, message.TransactionGuid);
                return;
            }

            var escrowTransaction = new EscrowTransaction
            {
                TransactionNumber = message.TransactionNumber,
                EscrowCode = message.EscrowCode,
                Amount = message.AmountPaid,
                CurrencyId = currency.Id,
                IsReleased = false,
                ServiceTypeId = ServiceType.MarketPlace.Id,
                TenantId = message.TenantId,
                TransactionGuid = message.TransactionGuid,
                Type = EscrowTransactionType.CodeRedemption,
                StatusId = EscrowTransactionStatus.InEscrow.Id,
                InDispute = false
            };

            var mAccount = await _context.Accounts
                .FirstOrDefaultAsync(x => x.AccountGuid == message.MerchantAccountId && x.TenantId == message.TenantId);

            if (mAccount != null)
            {
                var mett = new EscrowTransactionAccount
                {
                    AccountId = mAccount.Id,
                    EscrowTransactionId = escrowTransaction.Id,
                    TenantId = message.TenantId,
                    EscrowTransactionRoleId = EscrowTransactionRole.Merchant.Id
                };
                _context.EscrowTransactionAccounts.Add(mett);
                escrowTransaction.OwnerAccountId = mAccount.Id; // set owner account id from merchant account
            }
            else
            {
                _logger.LogCritical("---- Could not find Market Place merchant account with AccountGuid: {AccountGuid} -----", message.MerchantAccountId);
            }

            var cAccount = await _context.Accounts
                .FirstOrDefaultAsync(x => x.AccountGuid == message.CustomerAccountId && x.TenantId == message.TenantId);

            if (cAccount != null)
            {
                var cett = new EscrowTransactionAccount
                {
                    AccountId = cAccount.Id,
                    EscrowTransactionId = escrowTransaction.Id,
                    TenantId = message.TenantId,
                    EscrowTransactionRoleId = EscrowTransactionRole.Customer.Id
                };
                _context.EscrowTransactionAccounts.Add(cett);
                escrowTransaction.PayerAccountId = cAccount.Id; // set payer account id
            }
            else
            {
                _logger.LogCritical("---- Could not find Market Place customer account with AccountGuid: {AccountGuid} -----", message.CustomerAccountId);
            }

            var bAccount = await _context.Accounts
                .FirstOrDefaultAsync(x => x.AccountGuid == message.BrokerAccountId && x.TenantId == message.TenantId);

            if (bAccount != null)
            {
                var bett = new EscrowTransactionAccount
                {
                    AccountId = bAccount.Id,
                    EscrowTransactionId = escrowTransaction.Id,
                    TenantId = message.TenantId,
                    EscrowTransactionRoleId = EscrowTransactionRole.Broker.Id
                };
                _context.EscrowTransactionAccounts.Add(bett);
            }
            else
            {
                _logger.LogCritical("---- Could not find Market Place broker account with AccountGuid: {AccountGuid} -----", message.BrokerAccountId);
            }

            _context.EscrowTransactions.Add(escrowTransaction);

            if (message.Settlements?.Count > 0)
            {
                foreach (var s in message.Settlements)
                {
                    var settlement = new Settlement(message.TenantId, escrowTransaction.Id, s.Amount, s.AccountName, s.AccountNumber, s.BankCode, "");
                    _context.Settlements.Add(settlement);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}