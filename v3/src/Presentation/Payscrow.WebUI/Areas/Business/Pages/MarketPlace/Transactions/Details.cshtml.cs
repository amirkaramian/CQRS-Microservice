using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Payscrow.WebUI.Areas.Business.Pages.MarketPlace.Transactions
{
    public class DetailsModel : PageModel
    {
        public Guid TransactionId { get; set; }

        public void OnGet(Guid transId)
        {
            TransactionId = transId;
        }
    }
}