using Newtonsoft.Json;
using Payscrow.WebUI.Areas.Business.Common;
using Payscrow.WebUI.Areas.Business.Dtos.PaymentInvite;
using Payscrow.WebUI.Config;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Areas.Business.Services.PaymentInvite
{
    public class DealBusinessService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public DealBusinessService(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
        }

        public async Task<BaseDataTableResult<DealDto>> GetDealsAsync(DataTableRequest request, string currencyCode)
        {
            var url = UrlsConfig.PaymentInviteOperations.GetAccountDeals(_appSettings.Urls.PaymentInvite, currencyCode, request.PageIndex, request.PageSize);

            var searchModel = new DealSearchParameterModel();

            foreach (var column in request.Columns)
            {
                if (column.Data == "title" && !string.IsNullOrEmpty(column.Search.Value))
                {
                    searchModel.Title = column.Search.Value;
                }

                if (column.Data == "status" && !string.IsNullOrEmpty(column.Search.Value))
                {
                    searchModel.Status = column.Search.Value;
                }
            }

            var jsonObj = JsonConvert.SerializeObject(searchModel);

            var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var responseObj = JsonConvert.DeserializeObject<DataListResponse<DealDto>>(json);

            return new BaseDataTableResult<DealDto>(request.Draw, responseObj.RecordsTotal, responseObj.RecordsFiltered, responseObj.Data);
        }
    }

    public class DealSearchParameterModel
    {
        public string Title { get; set; }
        public string Status { get; set; }
    }
}