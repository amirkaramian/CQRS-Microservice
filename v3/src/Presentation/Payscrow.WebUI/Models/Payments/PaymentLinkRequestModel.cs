namespace Payscrow.WebUI.Models.Payments
{
    public class PaymentLinkRequestModel
    {
        public string paymentId { get; set; }
        public string PaymentMethodId { get; set; }
        public string ReturnUrl { get; set; }
    }
}
