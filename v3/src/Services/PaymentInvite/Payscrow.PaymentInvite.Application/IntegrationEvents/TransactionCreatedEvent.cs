using Payscrow.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.IntegrationEvents
{
    public class TransactionCreatedEvent : IntegrationEvent
    {
        public Guid DealId { get; }
        public string DealTitle { get; }
        public string DealDescription { get; }
        public Guid TransactionId { get; }
        public string BuyerEmail { get; }
        public string BuyerPhone { get; }

        public decimal TotalAmount { get; }
        public decimal SellerChargeAmount { get; }
        public decimal BuyerChargeAmount { get; }

        public TransactionCreatedEvent(Guid dealId,
            string dealTitle,
            string dealDescription,
            Guid transactionId,
            string buyerEmail,
            string buyerPhone,
            decimal totalAmount,
            decimal sellerChargeAmount,
            decimal buyerChargeAmount,
            Guid tenantId) : base(tenantId)
        {
            DealId = dealId;
            DealTitle = dealTitle;
            DealDescription = dealDescription;
            TransactionId = transactionId;
            BuyerEmail = buyerEmail;
            BuyerPhone = buyerPhone;
            TotalAmount = totalAmount;
            SellerChargeAmount = sellerChargeAmount;
            BuyerChargeAmount = buyerChargeAmount;
        }
    }
}