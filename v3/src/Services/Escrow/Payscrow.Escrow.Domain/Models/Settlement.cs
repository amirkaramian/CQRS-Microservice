using Payscrow.Escrow.Domain.Enumerations;
using System;

namespace Payscrow.Escrow.Domain.Models
{
    public class Settlement : Entity
    {
        public decimal Amount { get; private set; }
        public string BankAccountName { get; private set; }
        public string BankAccountNumber { get; private set; }
        public string BankCode { get; private set; }
        public string BankName { get; private set; }

        public SettlementStatus Status { get; private set; }

        public Guid EscrowTransactionId { get; private set; }
        public EscrowTransaction EscrowTransaction { get; set; }

        public Settlement(Guid tenantId, Guid escrowTransactionId, decimal amount, string bankAccountName,
            string bankAccountNumber, string bankCode, string bankName)
        {
            TenantId = tenantId;
            EscrowTransactionId = escrowTransactionId;
            Amount = amount;
            BankAccountName = bankAccountName;
            BankAccountNumber = bankAccountNumber;
            BankCode = bankCode;
            BankName = bankName;
            Status = SettlementStatus.Pending;
        }

        public void MarkAsInitiated()
        {
            Status = SettlementStatus.Initiated;
        }

        public void MarkAsSettled()
        {
            Status = SettlementStatus.Settled;
        }
    }
}