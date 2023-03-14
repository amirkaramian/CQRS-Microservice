using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Payscrow.ProjectMilestone.Application.Interfaces;
using Payscrow.ProjectMilestone.Application.Interfaces.Markers;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.ProjectMilestone.Api.Infrastructure.Services
{
    public class InviteNotificationService : IInviteNotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public InviteNotificationService(IHttpClientFactory httpClientFactory, ILogger<InviteNotificationService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("NotificationApi");
            _logger = logger;
        }

        public async Task SendAsync(InviteNotificationRequest request)
        {
            try
            {
                var invite = JsonConvert.SerializeObject(request);
                var requestContent = new StringContent(invite, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/v3/projectmilestone/send-invite", requestContent);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while sending project invite message");
            }           
        }
    }
}
