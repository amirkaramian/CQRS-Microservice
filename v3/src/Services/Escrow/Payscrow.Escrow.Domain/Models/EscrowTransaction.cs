using Payscrow.Escrow.Domain.Enumerations;
using System;
using System.Collections.Generic;

namespace Payscrow.Escrow.Domain.Models
{
    public class EscrowTransaction : Entity
    {
        public string TransactionNumber { get; set; }
        public string EscrowCode { get; set; }
        public decimal Amount { get; set; }
        public bool IsReleased { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public Guid TransactionGuid { get; set; }

        public EscrowTransactionType Type { get; set; }

        public int StatusId { get; set; }
        public EscrowTransactionStatus Status => Enumeration.FromValue<EscrowTransactionStatus>(StatusId);

        public bool InDispute { get; set; }

        public Guid OwnerAccountId { get; set; }
        public Guid PayerAccountId { get; set; }

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType => Enumeration.FromValue<ServiceType>(ServiceTypeId);

        public ICollection<EscrowTransactionAccount> EscrowTransactionAccounts { get; set; }
    }
}