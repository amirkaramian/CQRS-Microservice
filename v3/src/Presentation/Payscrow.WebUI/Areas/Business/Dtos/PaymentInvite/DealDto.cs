using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Areas.Business.Dtos.PaymentInvite
{
    public class DealDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public decimal SellerChargePercentage { get; set; }
        public string SellerVerificationCode { get; set; }

        public string BuyerLink { get; set; }

        public bool IsVerified { get; set; }

        public Guid CurrencyId { get; set; }

        public string Status { get; set; }
        public string CreateUtc { get; set; }

        public string Actions { get; set; }
    }
}