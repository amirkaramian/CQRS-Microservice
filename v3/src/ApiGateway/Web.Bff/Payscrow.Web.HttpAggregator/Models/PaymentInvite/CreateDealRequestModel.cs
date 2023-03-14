using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Web.HttpAggregator.Models.PaymentInvite
{
    public class CreateDealRequestModel
    {
        public string SellerEmail { get; set; }

        public string SellerCountryCode { get; set; }
        public string SellerLocalPhoneNumber { get; set; }
        public string CurrencyCode { get; set; }
        public decimal SellerChargePercentage { get; set; }

        public List<DealItemDto> Items { get; set; }

        public string BuyerUrl { get; set; }
    }

    public class DealItemDto
    {
        public decimal Amount { get; set; }
        public int AvailableQuantity { get; set; }
        public string Description { get; set; }
    }

    
}
