namespace Payscrow.Web.HttpAggregator.Models.PaymentInvite
{
    public class VerifyDealRequestModel
    {
        public string DealId { get; set; }
        public string Code { get; set; }
    }
}
