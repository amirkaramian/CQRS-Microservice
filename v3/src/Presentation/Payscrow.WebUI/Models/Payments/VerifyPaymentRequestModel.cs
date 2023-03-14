namespace Payscrow.WebUI.Models.Payments
{
    public class VerifyPaymentRequestModel
    {
        public string PaymentMethodId { get; set; }
        public string PaymentId { get; set; }
        public string ExternalTransactionId { get; set; }
        public string Status { get; set; }
    }
}
