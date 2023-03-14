using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Payscrow.WebUI.Pages.PaymentInvite
{
    public class PaymentStatusModel : PageModel
    {
        public string PaymentMethodId { get; set; }
        public string PaymentId { get; set; }
        public string TxRef { get; set; }
        public string ExternalTransactionId { get; set; }
        public string Status { get; set; }

        public void OnGet()
        {
            PaymentMethodId = Request.Query["payment_method_id"];
            PaymentId = Request.Query["payment_id"];
            TxRef = Request.Query["tx_ref"];
            ExternalTransactionId = Request.Query["transaction_id"];
            Status = Request.Query["status"];

            //Request.Query[""]
        }
    }
}
