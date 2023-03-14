using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Payscrow.WebUI.Pages.MarketPlace
{
    public class PaymentModel : PageModel
    {
        public Guid TransactionId { get; set; }

        public void OnGet(Guid transId)
        {
            TransactionId = transId;
        }
    }
}
