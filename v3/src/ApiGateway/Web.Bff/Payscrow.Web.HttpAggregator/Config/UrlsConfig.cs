using System.Collections.Generic;

namespace Payscrow.Web.HttpAggregator.Config
{

    public class UrlsConfig
    {

        public static class PaymentInviteOperations
        {
            public static string CreateDeal(string baseUrl) =>  $"{baseUrl}/api/v3/deals/create";
            public static string VerifyDeal(string baseUrl) => $"{baseUrl}/api/v3/deals/verify";

            public static string Currencies(string baseUrl) => $"{baseUrl}/api/v3/currencies";

            //// grpc call under REST must go trough port 80
            //public static string GetItemById(int id) => $"/api/v1/catalog/items/{id}";

            //public static string GetItemById(string ids) => $"/api/v1/catalog/items/ids/{string.Join(',', ids)}";

            //// REST call standard must go through port 5000
            //public static string GetItemsById(IEnumerable<int> ids) => $":5000/api/v1/catalog/items?ids={string.Join(',', ids)}";
        }

        //public class BasketOperations
        //{
        //    public static string GetItemById(string id) => $"/api/v1/basket/{id}";

        //    public static string UpdateBasket() => "/api/v1/basket";
        //}

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
