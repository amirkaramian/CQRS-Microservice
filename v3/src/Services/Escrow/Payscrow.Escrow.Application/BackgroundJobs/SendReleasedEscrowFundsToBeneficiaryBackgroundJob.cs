using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Payscrow.Escrow.Application.Common;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Domain.Enumerations;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.BackgroundJobs
{
    public class SendReleasedEscrowFundsToBeneficiaryBackgroundJob : IBackgroundJob, ISelfTransientLifetime
    {
        private readonly IEscrowDbContext _context;
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly ConfigOptions _configOptions;

        public SendReleasedEscrowFundsToBeneficiaryBackgroundJob(IEscrowDbContext context,
            ILogger<SendReleasedEscrowFundsToBeneficiaryBackgroundJob> logger,
            IHttpClientFactory httpClientFactory, ConfigOptions configOptions)
        {
            _context = context;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _configOptions = configOptions;
        }

        public async Task ExecuteAsync()
        {
            var releasedEscrowTransactions = await _context.EscrowTransactions
                .Where(x => x.IsReleased && x.StatusId == EscrowTransactionStatus.PendingSettlement.Id)
                .ToListAsync();

            var uri = $"{_configOptions.PaymentsUrl}/api/v3/settlements/initiate";

            foreach (var transaction in releasedEscrowTransactions)
            {
                _httpClient.DefaultRequestHeaders.Add("TenantId", transaction.TenantId.ToString());

                var settlements = await _context.Settlements.Where(x => x.EscrowTransactionId == transaction.Id
                                                                            && x.Status == SettlementStatus.Pending).ToListAsync();

                var payload = new
                {
                    transaction.TransactionGuid,
                    Settlements = settlements.ConvertAll(x => new
                    {
                        x.BankCode,
                        AccountNumber = x.BankAccountNumber,
                        x.Amount,
                        AccountName = x.BankAccountName
                    })
                };

                var commandContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(uri, commandContent);

                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonConvert.DeserializeObject<SendPaymentResponse>(responseString);
                    if (responseObject.success)
                    {
                        transaction.StatusId = EscrowTransactionStatus.InitiatedSettlement.Id;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }

    public class SendPaymentResponse
    {
        public bool success { get; set; }
    }
}