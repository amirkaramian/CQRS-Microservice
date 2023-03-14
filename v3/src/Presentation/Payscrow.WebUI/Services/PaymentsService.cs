using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Payscrow.WebUI.Config;
using Payscrow.WebUI.Models.Payments;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Services
{
    public class PaymentsService : ITransientLifetime
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentsService(IHttpClientFactory httpClientFactory,
            AppSettings appSettings,
            LinkGenerator linkGenerator,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient(HttpClientNameConstants.PAYMENTS);
            _appSettings = appSettings;
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpResponseMessage> GetPaymentMethods(string currencyCode, string accountId = null)
        {
            //var url = UrlsConfig.PaymentsOperations.GetPaymentMethodsByCurrencyCode(_appSettings.Urls.Payments, currencyCode, accountId);

            var response = await _httpClient.GetAsync($"/api/v3/payments/methods?currencyCode={currencyCode}&accountId={accountId}");
            return response;
        }

        public async Task<HttpResponseMessage> GetPaymentLink(PaymentLinkRequestModel model)
        {
            model.ReturnUrl = _linkGenerator.GetUriByPage(
                _httpContextAccessor.HttpContext,
                "/PaymentInvite/PaymentStatus",
                values: new { payment_id = model.paymentId, payment_method_id = model.PaymentMethodId });

            var url = UrlsConfig.PaymentsOperations.PaymentLink(_appSettings.Urls.Payments);

            var requestContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, requestContent);
        }

        public async Task<HttpResponseMessage> VerifyPayment(VerifyPaymentRequestModel model)
        {
            var url = UrlsConfig.PaymentsOperations.VerifyPayment(_appSettings.Urls.Payments);

            var requestContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, requestContent);
        }
    }
}