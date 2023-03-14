using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Payscrow.Core.Bus;
using Payscrow.Payments.Api.Application.IntegrationEvents.Publishing;
using Payscrow.Payments.Api.Application.Services;
using Payscrow.Payments.Api.Application.Utilities;
using Payscrow.Payments.Api.Data;
using Payscrow.Payments.Api.Domain.Enumerations;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Application.BackgroundJobs
{
    public class FlutterwaveTransferStatusCheckerBackgroundJob : IBackgroundJob, ISelfTransientLifetime
    {
        private readonly PaymentsDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppSettings _appSettings;
        private readonly IEventBus _eventBus;

        public FlutterwaveTransferStatusCheckerBackgroundJob(PaymentsDbContext context,
            IHttpClientFactory httpClientFactory, AppSettings appSettings, IEventBus eventBus)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _appSettings = appSettings;
            _eventBus = eventBus;
        }

        public async Task ExecuteAsync()
        {
            var initiatedSettlements = await _context.Settlements.Include(x => x.SettlementAccounts)
                                                    .Where(x => x.Status == SettlementStatus.Initiated && x.Provider == PaymentMethodProvider.Flutterwave)
                                                    .ToListAsync();

            var _httpClient = HttpFactory.InitHttpClient(_httpClientFactory, _appSettings.Flutterwave.BaseUrl)
                                .AddAuthorizationHeader("Bearer", _appSettings.Flutterwave.SecretKey)
                                .AddMediaType("application/json")
                                .AddHeader("cache-control", "no-cache");

            foreach (var settlement in initiatedSettlements)
            {
                var settlementAccounts = settlement.SettlementAccounts.ToList();

                var response = await _httpClient.GetAsync($"transfers?batch_id={settlement.GatewayReference}");

                var responseString = await response.Content.ReadAsStringAsync();

                var responseObj = JsonConvert.DeserializeObject<ResponseData>(responseString);

                if (responseObj?.status == "success")
                {
                    decimal amountConfirmed = 0;

                    foreach (var item in responseObj?.data)
                    {
                        if (item.status == "SUCCESSFUL")
                        {
                            var settlementAccount = settlementAccounts.Find(x => x.AccountNumber == item.account_number);

                            if (settlementAccount != null)
                            {
                                settlementAccount.Status = SettlementStatus.Completed;
                                amountConfirmed += settlementAccount.Amount;
                            }
                        }
                    }

                    var totalAmount = settlementAccounts.Sum(x => x.Amount);

                    if (totalAmount == amountConfirmed)
                    {
                        settlement.Status = SettlementStatus.Completed;
                    }
                }

                await _context.SaveChangesAsync();

                if (settlement.Status == SettlementStatus.Completed)
                    _eventBus.Publish(new BulkSettlementToBankAccountsCompletedIntegrationEvent(settlement.TenantId, settlement.TransactionGuid));
            }
        }

        public class ResponseData
        {
            public string status { get; set; }
            public string message { get; set; }
            public Meta meta { get; set; }
            public DataItem[] data { get; set; }

            public class Meta
            {
                public Page_Info page_info { get; set; }
            }

            public class Page_Info
            {
                public int total { get; set; }
                public int current_page { get; set; }
                public int total_pages { get; set; }
            }

            public class DataItem
            {
                public int id { get; set; }
                public string account_number { get; set; }
                public string bank_code { get; set; }
                public string full_name { get; set; }
                public DateTime created_at { get; set; }
                public string currency { get; set; }
                public object debit_currency { get; set; }
                public int amount { get; set; }
                public float fee { get; set; }
                public string status { get; set; }
                public string reference { get; set; }
                public string narration { get; set; }
                public object approver { get; set; }
                public string complete_message { get; set; }
                public int requires_approval { get; set; }
                public int is_approved { get; set; }
                public string bank_name { get; set; }
            }
        }
    }
}