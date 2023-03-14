using System;

namespace Payscrow.WebUI.Areas.Business.Pages.MarketPlace.Transactions
{
    public class OverviewModel : BaseTransactionPageModel
    {
        public void OnGet(Guid transId)
        {
            TransactionId = transId;
        }
    }
}