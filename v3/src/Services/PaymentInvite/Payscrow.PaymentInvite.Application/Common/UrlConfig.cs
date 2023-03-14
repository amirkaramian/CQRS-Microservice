namespace Payscrow.PaymentInvite.Application.Common
{
    public static class UrlConfig
    {
        public static class NotificationOperations
        {
            public static string SendVerificationCode(string baseUrl) => $"{baseUrl}/api/v3/verification/send-deal-code";
            
        }

        public static class PaymentOperations
        {
            public static string RequestPaymentLink(string baseUrl) => $"{baseUrl}/api/v3/payments/payment-link";
            public static string CreatePayment(string baseUrl) => $"{baseUrl}/api/v3/payments/create";
        }
    }
}
