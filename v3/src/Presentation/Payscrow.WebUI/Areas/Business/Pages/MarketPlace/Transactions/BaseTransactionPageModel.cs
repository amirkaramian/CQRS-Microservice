using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Payscrow.WebUI.Areas.Business.Pages.MarketPlace.Transactions
{
    public class BaseTransactionPageModel : PageModel
    {
        public Guid TransactionId { get; set; }
    }
}