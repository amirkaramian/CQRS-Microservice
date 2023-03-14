using Newtonsoft.Json;
using Payscrow.WebUI.BindingModels;
using Payscrow.WebUI.Config;
using Payscrow.WebUI.Models.Escrow;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;


        public UserService(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
        }


        public async Task<UserModel> GetUserModelAsync()
        {
            var url = UrlsConfig.EscrowOperations.User(_appSettings.Urls.Escrow);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserModel>(responseString);
        }


        public async Task<HttpResponseMessage> UpdateUserAsync(UserBindingModel model)
        {
            var url = UrlsConfig.EscrowOperations.UpdateUser(_appSettings.Urls.Escrow);

            var commandContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, commandContent);
        }
    }
}
