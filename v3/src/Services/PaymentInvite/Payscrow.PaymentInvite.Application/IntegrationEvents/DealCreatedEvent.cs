using Payscrow.Core.Events;
using System;

namespace Payscrow.PaymentInvite.Application.IntegrationEvents
{
    public class DealCreatedEvent : IntegrationEvent
    {
        public DealCreatedEvent(
            string dealId,
            string sellerEmail,
            string sellerPhone,
            string currencyCode,
            decimal sellerChargePercentage,
            string buyerLink, Guid tenantId) : base(tenantId)
        {
            DealId = dealId;
            SellerEmail = sellerEmail;
            SellerPhone = sellerPhone;
            CurrencyCode = currencyCode;
            SellerChargePercentage = sellerChargePercentage;
            BuyerLink = buyerLink;
        }

        public string DealId { get; }
        public string SellerEmail { get; }
        public string SellerPhone { get; }
        public string CurrencyCode { get; }
        public decimal SellerChargePercentage { get; set; }
        public string BuyerLink { get; }
    }
}