using Payscrow.Core.Events;
using System;

namespace Payscrow.Payments.Api.Application.IntegrationEvents
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
}