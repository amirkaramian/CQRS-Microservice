using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Payscrow.WebUI.Areas.Business.Pages.Deals
{
    
    public class TransactionDetailsModel : PageModel
    {
        public string TransactionId { get; set; }

        public void OnGet(string transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
