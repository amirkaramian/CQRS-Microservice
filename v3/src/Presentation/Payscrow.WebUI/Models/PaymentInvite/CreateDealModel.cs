using System;
using System.Collections.Generic;

namespace Payscrow.WebUI.Models.PaymentInvite
{
    public class CreateDealModel
    {
        public bool IsAuthenticated { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string BuyerPageUrl { get; set; }


        public List<CurrencyModel> Currencies { get; set; } = new List<CurrencyModel>();


        public class CurrencyModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Symbol { get; set; }
            public string Code { get; set; }

            public string DisplayName => $"({Code}) {Name}";
        }
    }
}
