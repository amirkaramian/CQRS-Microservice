using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Payscrow.Core.Interfaces;
using Payscrow.WebUI.Config;
using Payscrow.WebUI.Models.PaymentInvite;
using Payscrow.WebUI.Models.Payments;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Services
{
    public class PaymentInviteService 
    {
        private readonly IIdentityService _identityService;
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentInviteService(IIdentityService identityService,
            HttpClient httpClient, 
            AppSettings appSettings,
            LinkGenerator linkGenerator,
            IHttpContextAccessor httpContextAccessor)
        {
            _identityService = identityService;
            _httpClient = httpClient;
            _appSettings = appSettings;
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateDealModel> GetCreateDealDataModel()
        {
            var model = new CreateDealModel { 
                IsAuthenticated = _identityService.IsAuthenticated,
                Email = _identityService.Email,
                Phone = _identityService.Phone
            };            

            var url = UrlsConfig.PaymentInviteOperations.Currencies(_appSettings.Urls.PaymentInvite);

            var currencyResponse = await _httpClient.GetAsync(url);
            var data = JsonConvert.DeserializeObject<CurrencyResponse>(await currencyResponse.Content.ReadAsStringAsync());

            model.Currencies.AddRange(data.Currencies);

            return model;
        }

        public async Task<HttpResponseMessage> GetDealDetailsModel(string dealId)
        {
            var url = UrlsConfig.PaymentInviteOperations.DealDetails(_appSettings.Urls.PaymentInvite, dealId);

            var response = await _httpClient.GetAsync(url);
            return response;
        }


        public async Task<HttpResponseMessage> CreateNewDealAsync(CreateDealRequestModel model)
        {
            var url = UrlsConfig.PaymentInviteOperations.CreateDeal(_appSettings.Urls.PaymentInvite);

            var commandContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, commandContent);
        }


        public async Task<HttpResponseMessage> VerifyAnonymousDealAsync(VerifyDealRequestModel model)
        {
            var url = UrlsConfig.PaymentInviteOperations.VerifyDeal(_appSettings.Urls.PaymentInvite);

            var commandContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, commandContent);
        }


        public async Task<HttpResponseMessage> CreateNewTransactionAsync(CreateTransactionRequestModel model)
        {
            var url = UrlsConfig.PaymentInviteOperations.CreateTransaction(_appSettings.Urls.PaymentInvite);

            var commandContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, commandContent);
        }


        public async Task<HttpResponseMessage> GetPaymentLink(PaymentLinkRequestModel model)
        {
            model.ReturnUrl = _linkGenerator.GetUriByPage(
                _httpContextAccessor.HttpContext,
                "/PaymentInvite/PaymentStatus",
                values: new { local_transId = model.paymentId, payment_method_id = model.PaymentMethodId });

            var url = UrlsConfig.PaymentInviteOperations.GetPaymentLink(_appSettings.Urls.PaymentInvite);

            var requestContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, requestContent);
        }


        public class CurrencyResponse
        {
            public List<CreateDealModel.CurrencyModel> Currencies { get; set; }
        }
    }
}
