using Newtonsoft.Json;
using Payscrow.WebUI.Areas.Business.Common;
using Payscrow.WebUI.Areas.Business.Dtos.PaymentInvite;
using Payscrow.WebUI.Config;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Areas.Business.Services.PaymentInvite
{
    public class TransactionBusinessService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public TransactionBusinessService(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
        }

        public async Task<BaseDataTableResult<TransactionDto>> GetTransactionsAsync(DataTableRequest request, string currencyCode)
        {
            var url = UrlsConfig.PaymentInviteOperations.GetAccountTransactions(_appSettings.Urls.PaymentInvite, currencyCode, request.PageIndex, request.PageSize);

            var searchModel = new TransactionSearchParameterModel();

            foreach (var column in request.Columns)
            {
                if (column.Data == "statusId" && !string.IsNullOrEmpty(column.Search.Value))
                {
                    searchModel.StatusId = column.Search.Value;
                }

                if (column.Data == "paymentStatus" && !string.IsNullOrEmpty(column.Search.Value))
                {
                    searchModel.PaymentStatus = column.Search.Value;
                }
            }

            var jsonObj = JsonConvert.SerializeObject(searchModel);

            var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var responseObj = JsonConvert.DeserializeObject<DataListResponse<TransactionDto>>(json);

            return new BaseDataTableResult<TransactionDto>(request.Draw, responseObj.RecordsTotal, responseObj.RecordsFiltered, responseObj.Data);
        }

        public async Task<HttpResponseMessage> GetSellerTransactionDetail(string transactionId)
        {
            var url = UrlsConfig.PaymentInviteOperations.GetSellerTransactionDetail(_appSettings.Urls.PaymentInvite, transactionId);

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return response;
        }
    }

    public class TransactionSearchParameterModel
    {
        public string StatusId { get; set; }
        public string PaymentStatus { get; set; }
    }
}