namespace Payscrow.WebUI.Config
{
    public class UrlsConfig
    {
        public static class EscrowOperations
        {
            public static string Currencies(string baseUrl) => $"{baseUrl}/api/v3/currencies";

            public static string AccountSettings(string baseUrl) => $"{baseUrl}/api/v3/accountsetting";

            public static string User(string baseUrl) => $"{baseUrl}/api/v3/user";

            public static string UpdateUser(string baseUrl) => $"{baseUrl}/api/v3/user/update";

            public static string ChangeCurrency(string baseUrl, string currencyCode) => $"{baseUrl}/api/v3/accountsetting/change-currency?currencyCode={currencyCode}";
        }

        public static class PaymentInviteOperations
        {
            public static string GetAccountDeals(string baseUrl, string currencyCode, int? page = 1, int? pageSize = 10)
                => $"{baseUrl}/api/v3/deals?page={page}&pagesize={pageSize}&currencyCode={currencyCode}";

            public static string GetAccountTransactions(string baseUrl, string currencyCode, int? page = 1, int? pageSize = 10)
                => $"{baseUrl}/api/v3/transactions?page={page}&pagesize={pageSize}&currencyCode={currencyCode}";

            public static string GetSellerTransactionDetail(string baseUrl, string transactionId)
                => $"{baseUrl}/api/v3/transactions/seller/detail/{transactionId}";

            public static string CreateDeal(string baseUrl) => $"{baseUrl}/api/v3/deals/create";

            public static string DealDetails(string baseUrl, string dealId) => $"{baseUrl}/api/v3/deals/details?dealId={dealId}";

            public static string VerifyDeal(string baseUrl) => $"{baseUrl}/api/v3/deals/verify";

            public static string CreateTransaction(string baseUrl) => $"{baseUrl}/api/v3/transactions/create";

            public static string Currencies(string baseUrl) => $"{baseUrl}/api/v3/currencies";

            public static string GetPaymentLink(string baseUrl) => $"{baseUrl}/api/v3/transactions/payment-link";
        }

        public static class PaymentsOperations
        {
            public static string GetPaymentMethodsByCurrencyCode(string baseUrl, string currencyCode, string accountId = null) =>
                string.IsNullOrEmpty(accountId) ? $"{baseUrl}/api/v3/payments/methods?currencycode={currencyCode}"
                : $"{baseUrl}/api/v3/payments/methods?currencyCode={currencyCode}&accountId={accountId}";

            public static string VerifyPayment(string baseUrl) => $"{baseUrl}/api/v3/payments/verify";

            public static string PaymentLink(string baseUrl) => $"{baseUrl}/api/v3/payments/payment-link";
        }

        //public class OrdersOperations
        //{
        //    public static string GetOrderDraft() => "/api/v1/orders/draft";
        //}

        //public string Basket { get; set; }

        //public string Catalog { get; set; }

        //public string Orders { get; set; }

        //public string GrpcBasket { get; set; }

        //public string GrpcCatalog { get; set; }

        public string GrpcPaymentInvite { get; set; }
    }
}