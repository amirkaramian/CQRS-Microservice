using Newtonsoft.Json;
using Payscrow.WebUI.Areas.Business.Common;
using Payscrow.WebUI.Areas.Business.Dtos.MarketPlace;
using Payscrow.WebUI.Services;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Areas.Business.Services.MarketPlace
{
    public class MarketPlaceTransactionService : ITransientLifetime
    {
        private readonly HttpClient _httpClient;

        public MarketPlaceTransactionService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(HttpClientNameConstants.MARKET_PLACE);
        }

        public async Task<BaseDataTableResult<TransactionDto>> GetTransactionsAsync(DataTableRequest request, string currencyCode)
        {
            var url = $"/api/v3/transactions?page={request.PageIndex}&pagesize={request.PageSize}&currencyCode={currencyCode}";

            dynamic searchModel = new { };

            foreach (var column in request.Columns)
            {
                if (column.Data == "statusId" && !string.IsNullOrEmpty(column.Search.Value))
                {
                    searchModel.StatusId = column.Search.Value;
                }

                //if (column.Data == "paymentStatus" && !string.IsNullOrEmpty(column.Search.Value))
                //{
                //    searchModel.PaymentStatus = column.Search.Value;
                //}
            }

            var jsonObj = JsonConvert.SerializeObject(searchModel);

            var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var responseObj = JsonConvert.DeserializeObject<BaseDataTableResult<TransactionDto>>(json);

            return new BaseDataTableResult<TransactionDto>(request.Draw, responseObj.RecordsTotal, responseObj.RecordsFiltered, responseObj.Data);
        }
    }
}