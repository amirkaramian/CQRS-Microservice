namespace Payscrow.ProjectMilestone.Application.Common.Helpers
{
    public static class UrlConfigHelper
    {
        public static class NotificationOperations
        {
            public static string SendProjectPartnerInvite(string baseUrl) => $"{baseUrl}/api/v3/projectmilestone/send-invite";

        }

        public static class PaymentOperations
        {
            public static string RequestPaymentLink(string baseUrl) => $"{baseUrl}/api/v3/payments/payment-link";
            public static string CreatePayment(string baseUrl) => $"{baseUrl}/api/v3/payments/create";
        }
    }
}
