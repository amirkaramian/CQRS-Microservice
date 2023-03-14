using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Payscrow.MarketPlace.Application.Common;
using Payscrow.MarketPlace.Application.Domain.Entities;
using Payscrow.MarketPlace.Application.Domain.Enumerations;
using Payscrow.MarketPlace.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.Commands.Business.Transactions
{
    public static class CreateTransaction
    {
        public class Command : BaseCommand<CommandResult>
        {
            public Guid BrokerAccountId { get; set; }
            public string BrokerName { get; set; }
            public string TransactionReference { get; set; }
            public string MerchantEmailAddress { get; set; }
            public string MerchantPhoneNo { get; set; }
            public string MerchantName { get; set; }
            public string CustomerEmailAddress { get; set; }
            public string CustomerPhoneNo { get; set; }
            public string CustomerName { get; set; }
            public string CurrencyCode { get; set; }
            public decimal MerchantChargePercentage { get; set; }
            public string ReturnUrl { get; set; }
            public string ResponseUrl { get; set; }

            public List<RequestItem> Items { get; set; } = new List<RequestItem>();
            public List<SettlementAccountDto> SettlementAccounts { get; set; } = new List<SettlementAccountDto>();

            public class RequestItem
            {
                public string Name { get; set; }
                public string Description { get; set; }
                public int Quantity { get; set; }
                public decimal Price { get; set; }
            }

            public class SettlementAccountDto
            {
                public string BankCode { get; set; }
                public string AccountNumber { get; set; }
                public string AccountName { get; set; }
                public decimal Amount { get; set; }
            }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly IMarketPlaceDbContext _context;
                private readonly IChargeService _chargeService;
                private readonly HttpClient _httpClient;
                private readonly ConfigSetting _configSetting;

                public Handler(IMarketPlaceDbContext context, IChargeService chargeService, IHttpClientFactory httpClientFactory, ConfigSetting configSetting)
                {
                    _context = context;
                    _chargeService = chargeService;
                    _httpClient = httpClientFactory.CreateClient();
                    _configSetting = configSetting;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var currencyId = await _context.Currencies.ForTenant(request.TenantId)
                        .Where(x => x.Code == request.CurrencyCode && x.IsActive)
                        .Select(x => x.Id).FirstOrDefaultAsync();

                    if (currencyId == Guid.Empty)
                    {
                        result.Errors.Add(("CurrencyCode", "invalid currency code"));
                        return result;
                    }

                    if (await _context.Transactions.AnyAsync(x => x.BrokerTransactionReference == request.TransactionReference && x.TenantId == request.TenantId))
                    {
                        result.Errors.Add(("TransactionReference", $"Transaction Reference: \"{request.TransactionReference}\" already exist."));
                        return result;
                    }

                    var identityAccountCreateUrl = $"{_configSetting.IdentityUrl}/api/v3/accounts/quick-create";
                    _httpClient.Timeout = TimeSpan.FromMinutes(10);

                    var merchantPayload = new
                    {
                        EmailAddress = request.MerchantEmailAddress,
                        Name = request.MerchantName,
                        PhoneNumber = request.MerchantPhoneNo,
                        request.TenantId
                    };

                    var merchantContent = new StringContent(JsonConvert.SerializeObject(merchantPayload), Encoding.UTF8, "application/json");
                    var merchantResponse = await _httpClient.PostAsync(identityAccountCreateUrl, merchantContent, cancellationToken);
                    var merchantResponseString = await merchantResponse.Content.ReadAsStringAsync(cancellationToken);
                    var merchantAccount = JsonConvert.DeserializeObject<AccountDto>(merchantResponseString);

                    const int seedNumber = 5893248;
                    var maxNumber = (await _context.Transactions.MaxAsync(x => (long?)x.FriendlyNumber, cancellationToken) ?? seedNumber) + 1;

                    var transaction = new Transaction
                    {
                        TenantId = request.TenantId,
                        BrokerAccountId = request.BrokerAccountId,
                        MerchantAccountId = merchantAccount.Id,
                        BrokerName = request.BrokerName,
                        BrokerTransactionReference = request.TransactionReference,
                        Number = $"MKT-{maxNumber:D8}",
                        FriendlyNumber = maxNumber,
                        MerchantEmailAddress = request.MerchantEmailAddress,
                        MerchantName = string.IsNullOrEmpty(request.MerchantName)
                                                    ? merchantAccount.Name : request.MerchantName,
                        MerchantPhone = request.MerchantPhoneNo,
                        StatusId = TransactionStatus.Pending.Id,
                        PaymentStatus = TransactionPaymentStatus.Pending,
                        CurrencyId = currencyId
                    };

                    if (!string.IsNullOrEmpty(request.CustomerEmailAddress))
                    {
                        var customerPayload = new
                        {
                            emailAddress = request.CustomerEmailAddress,
                            name = request.CustomerName,
                            phoneNumber = request.CustomerPhoneNo,
                            tenantId = request.TenantId
                        };

                        var customerContent = new StringContent(JsonConvert.SerializeObject(customerPayload), Encoding.UTF8, "application/json");
                        var customerResponse = await _httpClient.PostAsync(identityAccountCreateUrl, customerContent, cancellationToken);
                        var customerResponseString = await customerResponse.Content.ReadAsStringAsync(cancellationToken);
                        var customerAccount = JsonConvert.DeserializeObject<AccountDto>(customerResponseString);

                        transaction.CustomerAccountId = customerAccount.Id;
                        transaction.CustomerEmailAddress = request.CustomerEmailAddress;
                        transaction.CustomerName = string.IsNullOrEmpty(request.CustomerName)
                            ? customerAccount.Name : request.CustomerName;
                        transaction.CustomerPhone = request.CustomerPhoneNo;
                    }

                    var items = new List<Item>();

                    foreach (var requestItem in request.Items)
                    {
                        var item = new Item
                        {
                            TenantId = request.TenantId,
                            Price = requestItem.Price,
                            Description = requestItem.Description,
                            Name = requestItem.Name,
                            Quantity = requestItem.Quantity,
                            TransactionId = transaction.Id
                        };
                        items.Add(item);
                    }

                    var totalAmount = items.Sum(x => x.Quantity * x.Price);
                    var charge = await _chargeService.CalculateChargesAsync(request.TenantId, currencyId, totalAmount, request.BrokerAccountId);

                    transaction.GrandTotalPayable = totalAmount + charge.Charge;
                    transaction.MerchantCharge = charge.Charge * (request.MerchantChargePercentage / 100);
                    transaction.CustomerCharge = charge.Charge * ((100 - request.MerchantChargePercentage) / 100);

                    if (request.SettlementAccounts != null && request.SettlementAccounts.Count > 0)
                    {
                        var totalSum = request.SettlementAccounts.Sum(x => x.Amount);

                        if (totalSum > totalAmount)
                        {
                            result.Errors.Add(("Settlement Amount", "Settlement sum cannot be greater than the amount to be paid"));
                            return result;
                        }

                        var settlementAccountList = new List<SettlementAccount>();
                        foreach (var settlementAccount in request.SettlementAccounts)
                        {
                            var account = new SettlementAccount
                            {
                                TenantId = request.TenantId,
                                AccountName = settlementAccount.AccountName,
                                AccountNumber = settlementAccount.AccountNumber,
                                BankCode = settlementAccount.BankCode,
                                Amount = settlementAccount.Amount,
                                TransactionId = transaction.Id
                            };
                            settlementAccountList.Add(account);
                        }
                        _context.SettlementAccounts.AddRange(settlementAccountList);
                    }

                    _context.Transactions.Add(transaction);
                    _context.Items.AddRange(items);

                    var statusLog = new TransactionStatusLog
                    {
                        TransactionId = transaction.Id,
                        StatusId = transaction.StatusId,
                        InDispute = transaction.InDispute,
                        InEscrow = transaction.InEscrow,
                        Note = "Transaction Created"
                    };
                    _context.TransactionStatusLogs.Add(statusLog);

                    await _context.SaveChangesAsync(cancellationToken);

                    result.TransactionId = transaction.Id;
                    result.PaymentLink = $"{request.ReturnUrl}?transId={transaction.Id}";
                    return result;
                }
            }
        }

        public class CommandResult : BaseCommandResult
        {
            public Guid TransactionId { get; set; }
            public string PaymentLink { get; set; }
        }

        public class CommandValidator : BaseCommandValidator<Command, CommandResult>
        {
            public CommandValidator()
            {
                RuleFor(x => x.BrokerAccountId).NotEmpty();
                RuleFor(x => x.BrokerName).NotEmpty();
                RuleFor(x => x.TransactionReference).NotEmpty().MaximumLength(200);
                RuleFor(x => x.CurrencyCode).NotEmpty().Length(3);
                RuleFor(x => x.MerchantEmailAddress).NotEmpty().EmailAddress();
                RuleFor(x => x.MerchantName).NotEmpty().MaximumLength(300);
                RuleFor(x => x.CustomerEmailAddress).NotEmpty().EmailAddress();
                RuleFor(x => x.CustomerName).NotEmpty().MaximumLength(300);
                RuleFor(x => x.MerchantChargePercentage).LessThanOrEqualTo(100).GreaterThanOrEqualTo(0);

                RuleFor(x => x.Items).Must(x => x.Count > 0).WithMessage("must have at least one item to pay for.");

                RuleForEach(x => x.Items).SetValidator(new ItemValidator());
                RuleForEach(x => x.SettlementAccounts).SetValidator(new SettlementAccountValidator());
            }

            public class ItemValidator : AbstractValidator<Command.RequestItem>
            {
                public ItemValidator()
                {
                    RuleFor(x => x.Name).NotEmpty().MaximumLength(300);
                    RuleFor(x => x.Description).MaximumLength(4000);
                    RuleFor(x => x.Price).GreaterThan(0).NotEmpty();
                    RuleFor(x => x.Quantity).GreaterThan(0).NotEmpty();
                }
            }

            public class SettlementAccountValidator : AbstractValidator<Command.SettlementAccountDto>
            {
                public SettlementAccountValidator()
                {
                    RuleFor(x => x.AccountNumber).NotEmpty().Length(10);
                    RuleFor(x => x.BankCode).NotEmpty().Length(3);
                    RuleFor(x => x.AccountName).NotEmpty().MaximumLength(100);
                    RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
                }
            }
        }

        public class AccountDto
        {
            public Guid Id { get; set; }
            public long Number { get; set; }
            public string Name { get; set; }
            public bool IsVerified { get; set; }
        }
    }
}