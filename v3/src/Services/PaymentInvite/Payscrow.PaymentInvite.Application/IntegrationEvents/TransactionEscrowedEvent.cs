using Payscrow.Core.Events;
using System;

namespace Payscrow.PaymentInvite.Application.IntegrationEvents
{
    public class TransactionEscrowedEvent : IntegrationEvent
    {
        public TransactionEscrowedEvent(Guid accountGuid, decimal amountPaid, decimal escrowCharge, string currencyCode, Guid tenantId)
            : base(tenantId)
        {
            AccountGuid = accountGuid;
            AmountPaid = amountPaid;
            EscrowCharge = escrowCharge;
            CurrencyCode = currencyCode;
        }

        public Guid AccountGuid { get; }
        public decimal EscrowCharge { get; }
        public decimal AmountPaid { get; }
        public string CurrencyCode { get; set; }
    }
}