using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Models.PaymentInvite
{
    public class CreateTransactionRequestModel
    {
        public string DealId { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerPhone { get; set; }
    }
}
