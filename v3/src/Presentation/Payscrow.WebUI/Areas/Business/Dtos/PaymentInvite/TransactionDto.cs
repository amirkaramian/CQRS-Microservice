using System;

namespace Payscrow.WebUI.Areas.Business.Dtos.PaymentInvite
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public string PaymentStatus { get; set; }

        public bool InEscrow { get; set; }

        public Guid? BuyerId { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerPhoneLocalNumber { get; set; }

        public Guid DealId { get; set; }
        public string DealTitle { get; set; }
        public string DealDescription { get; set; }
        public string DealCurrencySymbol { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal SellerChargeAmount { get; set; }
        public decimal BuyerChargeAmount { get; set; }

        public DateTime CreateUtc { get; set; }

        public string Actions { get; set; }
    }
}