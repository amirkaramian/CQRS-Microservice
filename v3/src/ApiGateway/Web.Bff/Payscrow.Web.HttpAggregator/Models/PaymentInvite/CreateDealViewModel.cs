using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Web.HttpAggregator.Models.PaymentInvite
{
    public class CreateDealViewModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }

        public string BuyerHostName { get; set; }


        public List<CurrencyModel> Currencies { get; set; }


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
