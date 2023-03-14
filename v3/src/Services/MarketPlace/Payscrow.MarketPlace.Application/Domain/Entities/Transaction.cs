using Payscrow.MarketPlace.Application.Domain.Enumerations;
using System;
using System.Collections.Generic;

namespace Payscrow.MarketPlace.Application.Domain.Entities
{
    public class Transaction : Entity, IAuditableEntity
    {
        public string Number { get; set; }
        public long FriendlyNumber { get; set; }

        public Guid BrokerAccountId { get; set; }
        public string BrokerTransactionReference { get; set; }
        public string BrokerName { get; set; }
        public decimal BrokerFee { get; set; }

        public Guid MerchantAccountId { get; set; }
        public string MerchantName { get; set; }
        public string MerchantEmailAddress { get; set; }
        public string MerchantPhone { get; set; }
        public decimal MerchantCharge { get; set; }

        public Guid? CustomerAccountId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string CustomerPhone { get; set; }
        public decimal CustomerCharge { get; set; }

        public decimal GrandTotalPayable { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public int StatusId { get; set; }
        public bool InEscrow { get; set; }
        public bool InDispute { get; set; }
        public string EscrowCode { get; set; }

        public TransactionPaymentStatus PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }

        public string WebhookNotificationUrl { get; set; }

        public ICollection<Item> Items { get; set; }
        public ICollection<SettlementAccount> SettlementAccounts { get; set; }
        public ICollection<TransactionStatusLog> TransactionStatusLogs { get; set; }

        public Guid CreateUserId { get; set; }
        public DateTime CreateUtc { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime? UpdateUtc { get; set; }

        public Transaction()
        {
            CreateUtc = DateTime.UtcNow;
        }
    }
}