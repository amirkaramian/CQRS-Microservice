using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Payscrow.MarketPlace.Application.Common;
using Payscrow.MarketPlace.Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.Commands.Business.Transactions
{
    public static class CreateTransactionPayment
    {
        public class Command : BaseCommand<CommandResult>
        {
            public Guid? TransactionId { get; set; }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly HttpClient _httpClient;
                private readonly MarketPlaceDbContext _context;
                private readonly ConfigSetting _configSetting;

                public Handler(IHttpClientFactory httpClientFactory, MarketPlaceDbContext context, ConfigSetting configSetting)
                {
                    _httpClient = httpClientFactory.CreateClient();
                    _context = context;
                    _configSetting = configSetting;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var transaction = await _context.Transactions.Include(x => x.Currency).FirstOrDefaultAsync(x => x.Id == request.TransactionId && x.TenantId == request.TenantId);

                    if (transaction is null)
                    {
                        result.Errors.Add(("TransactionId", "Transaction not found"));
                        return result;
                    }

                    var payload = new
                    {
                        Amount = transaction.GrandTotalPayable,
                        TransactionGuid = transaction.Id,
                        Name = transaction.CustomerName,
                        EmailAddress = transaction.CustomerEmailAddress,
                        CurrencyCode = transaction.Currency?.Code
                    };

                    var commandContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                    _httpClient.DefaultRequestHeaders.Add("TenantId", request.TenantId.ToString());
                    var uri = $"{_configSetting.PaymentsUrl}/api/v3/payments/create";

                    var response = await _httpClient.PostAsync(uri, commandContent);

                    var responseString = await response.Content.ReadAsStringAsync();

                    var responseObject = JsonConvert.DeserializeObject<CreatePaymentResponseModel>(responseString);

                    if (responseObject.Success)
                    {
                        result.PaymentId = responseObject.PaymentId;
                    }
                    else
                    {
                        foreach (var error in responseObject.Errors)
                        {
                            result.Errors.Add((error.Field, error.Message));
                        }
                    }

                    return result;
                }
            }
        }

        public class CreatePaymentResponseModel
        {
            public bool Success { get; set; }
            public Guid PaymentId { get; set; }
            public List<Error> Errors { get; set; } = new List<Error>();

            public class Error
            {
                public string Field { get; set; }
                public string Message { get; set; }
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.TransactionId).NotEmpty();
                RuleFor(x => x.TenantId).NotEmpty();
            }
        }

        public class CommandResult : BaseCommandResult
        {
            public Guid PaymentId { get; set; }
        }
    }
}