using Newtonsoft.Json;
using Payscrow.PaymentInvite.Grpc;
using Payscrow.Web.HttpAggregator.Config;
using Payscrow.Web.HttpAggregator.Models.PaymentInvite;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Web.HttpAggregator.Services
{
    public class PaymentInviteService : IPaymentInviteService
    {
        //private readonly UrlsConfig _urls;

        //public PaymentInviteService(UrlsConfig urls)
        //{
        //    _urls = urls;
        //}
        private readonly HttpClient _apiClient;
        private readonly IIdentityService _identityService;

        public PaymentInviteService(HttpClient apiClient, IIdentityService identityService)
        {
            _apiClient = apiClient;
            _identityService = identityService;
        }


        public async Task<CreateDealViewModel> GetCreateDealDataModel()
        {
            var model = new CreateDealViewModel {
                Email = _identityService.GetUserEmail(),
                Phone = _identityService.GetUserPhone()
            };

            var url = UrlsConfig.PaymentInviteOperations.Currencies("http://paymentinvite.api");

            var result = await _apiClient.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<CreateDealViewModel>(await result.Content.ReadAsStringAsync());
                model.Currencies = res.Currencies;
            }            

            return model;
        }


        public async Task<HttpResponseMessage> CreateNewDealAsync(CreateDealRequestModel model)
        {
            var url =  UrlsConfig.PaymentInviteOperations.CreateDeal("http://paymentinvite.api");

            var commandContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            return await _apiClient.PostAsync(url, commandContent);            
        }


        public async Task<HttpResponseMessage> VerifyAnonymousDealAsync(VerifyDealRequestModel model)
        {
            var url = UrlsConfig.PaymentInviteOperations.VerifyDeal("http://paymentinvite.api");

            var commandContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            return await _apiClient.PostAsync(url, commandContent);
        }


        private CreateInviteResponseModel MapToResponse(CreateInviteCommandResult result)
        {
            return new CreateInviteResponseModel
            {
                InviteId = result.InviteId
            };
        }

        private CreateInviteCommand MapToCreateInviteCommand(CreateDealRequestModel model)
        {
            var command = new CreateInviteCommand
            {                //BuyerCountryCode = model.BuyerCountryCode,
                //BuyerLocalPhoneNumber = model.BuyerLocalPhoneNumber,
                BuyerCountryCode = "test_contry code",
                BuyerLocalPhoneNumber = "test",
                SellerChargePercentage = (float)model.SellerChargePercentage,
                SellerEmail = model.SellerEmail,
                //SellerCountryCode = model.SellerCountryCode,
                SellerCountryCode = "Seller Contry code",
                //SellerLocalPhoneNumber = model.SellerLocalPhoneNumber,
                SellerLocalPhoneNumber = "Local Phone for seller",
                CurrencyCode = model.CurrencyCode
            };

            var items = model.Items?.Select(x => new PaymentInvite.Grpc.TradeItemDto { Amount = (float)x.Amount, Description = x.Description, Quantity = x.AvailableQuantity }).ToList();

            command.Items.AddRange(items);

            return command;
        }

 
    }
}
