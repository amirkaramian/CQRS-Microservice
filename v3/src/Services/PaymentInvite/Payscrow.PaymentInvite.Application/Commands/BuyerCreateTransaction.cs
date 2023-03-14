using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Payscrow.Core.Bus;
using Payscrow.PaymentInvite.Application.Common;
using Payscrow.PaymentInvite.Application.IntegrationEvents;
using Payscrow.PaymentInvite.Application.Interfaces;
using Payscrow.PaymentInvite.Application.Services;
using Payscrow.PaymentInvite.Domain.Enumerations;
using Payscrow.PaymentInvite.Domain.Models;
using Payscrow.PaymentInvite.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.Commands
{
    public static class BuyerCreateTransaction
    {
        public class Command : BaseCommand, IRequest<CommandResult>
        {
            public Guid? DealId { get; set; }
            public string BuyerEmail { get; set; }
            public string BuyerPhone { get; set; }

            //public List<TransactionItemModel> Items { get; set; }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly IPaymentInviteDbContext _context;
                private readonly IChargeCalculatorService _chargeCalculatorService;
                private readonly IEventBus _eventBus;
                private readonly HttpClient _httpClient;
                private readonly ConfigOptions _configOptions;
                private readonly TransactionService _transactionService;

                public Handler(IPaymentInviteDbContext context,
                    IChargeCalculatorService chargeCalculatorService,
                    IEventBus eventBus,
                    IHttpClientFactory httpClientFactory,
                    ConfigOptions configOptions, TransactionService transactionService)
                {
                    _context = context;
                    _chargeCalculatorService = chargeCalculatorService;
                    _eventBus = eventBus;
                    _httpClient = httpClientFactory.CreateClient();
                    _configOptions = configOptions;
                    _transactionService = transactionService;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var deal = await _context.Deals
                        .Include(x => x.Items)
                        .Include(x => x.Currency)
                        .SingleOrDefaultAsync(x => x.Id == request.DealId);

                    if (deal == null)
                    {
                        result.Errors.Add(("DealId", $"deal not found with Id: {request.DealId}"));
                        return result;
                    }

                    if (deal.Status != DealStatus.Active)
                    {
                        result.Errors.Add(("DealStatus", $"this deal is {deal.Status}!"));
                        return result;
                    }

                    var actualTotalAmount = deal.Items.Sum(x => x.Quantity * x.Amount);
                    var buyerChargeAmount = await _chargeCalculatorService.GetChargeAsync(actualTotalAmount, (100 - deal.SellerChargePercentage), deal.Currency?.Code);

                    var trxNo = await _transactionService.GetNextTransactionNumberAsync();

                    var transaction = new Transaction
                    {
                        DealId = deal.Id,
                        CurrencyId = deal.CurrencyId,
                        Number = trxNo,
                        BuyerEmail = request.BuyerEmail,
                        BuyerPhone = new PhoneNumber("", request.BuyerPhone),
                        StatusId = TransactionStatus.Pending.Id,
                        SellerChargeAmount = await _chargeCalculatorService.GetChargeAsync(actualTotalAmount, deal.SellerChargePercentage, deal.Currency?.Code),
                        BuyerChargeAmount = buyerChargeAmount,
                        TotalAmount = actualTotalAmount + buyerChargeAmount
                    };

                    var transactionItems = deal.Items.Select(x => new TransactionItem
                    {
                        Quantity = x.Quantity,
                        Description = x.Description,
                        DealItemId = x.Id,
                        Name = x.Name,
                        Amount = x.Amount
                    }).ToList();

                    transaction.Items.AddRange(transactionItems);

                    var transactionStatusLog = new TransactionStatusLog
                    {
                        TransactionId = transaction.Id,
                        TransactionStatusId = TransactionStatus.Pending.Id,
                        Comment = "Transaction initiated."
                    };

                    _context.TransactionStatusLogs.Add(transactionStatusLog);

                    var createPaymentRequestModel = new CreatePaymentRequestModel
                    {
                        Amount = transaction.TotalAmount,
                        TransactionGuid = transaction.Id,
                        Name = transaction.BuyerEmail,
                        EmailAddress = transaction.BuyerEmail,
                        PhoneNumber = transaction.BuyerPhone.ToString(),
                        CurrencyCode = deal.Currency?.Code
                    };

                    var queryContent = new StringContent(JsonConvert.SerializeObject(createPaymentRequestModel),
                        Encoding.UTF8, "application/json");

                    var requestUrl = UrlConfig.PaymentOperations.CreatePayment(_configOptions.PaymentsUrl);

                    var response = await _httpClient.PostAsync(requestUrl, queryContent);

                    var responseObject = JsonConvert.DeserializeObject<CreatePaymentResponseModel>(await response.Content.ReadAsStringAsync());

                    if (responseObject.IsSuccessful)
                    {
                        _context.Transactions.Add(transaction);

                        await _context.SaveChangesAsync();

                        _eventBus.Publish(new TransactionCreatedEvent(
                            deal.Id,
                            deal.Title,
                            deal.Description,
                            transaction.Id,
                            transaction.BuyerEmail,
                            transaction.BuyerPhone.ToString(),
                            transaction.TotalAmount,
                            transaction.SellerChargeAmount,
                            transaction.BuyerChargeAmount,
                            request.TenantId));

                        result.TransactionId = transaction.Id;
                        result.PaymentId = responseObject.PaymentId;
                    }
                    else
                    {
                        result.Errors.AddRange(responseObject.Errors);
                    }

                    return result;
                }
            }

            //public class TransactionItemModel
            //{
            //    public int Quantity { get; set; }
            //    public string Description { get; set; }

            //    public Guid? DealItemId { get; set; }
            //}
        }

        public class CommandResult : BaseResult
        {
            public Guid TransactionId { get; set; }
            public Guid PaymentId { get; set; }
        }

        public class CreatePaymentRequestModel
        {
            public decimal Amount { get; set; }
            public Guid TransactionGuid { get; set; }
            public string Name { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string CurrencyCode { get; set; }
        }

        public class CreatePaymentResponseModel
        {
            public bool IsSuccessful { get; set; }
            public Guid PaymentId { get; set; }
            public List<(string, string)> Errors { get; set; } = new List<(string, string)>();
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DealId).NotEmpty();
                RuleFor(x => x.BuyerEmail).NotEmpty().EmailAddress();
                RuleFor(x => x.BuyerPhone).NotEmpty();
            }
        }
    }
}