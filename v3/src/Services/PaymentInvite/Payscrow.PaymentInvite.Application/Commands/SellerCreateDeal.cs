using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Payscrow.Core.Bus;
using Payscrow.Core.Interfaces;
using Payscrow.PaymentInvite.Application.Common;
using Payscrow.PaymentInvite.Application.Common.Utilities;
using Payscrow.PaymentInvite.Application.IntegrationEvents;
using Payscrow.PaymentInvite.Application.Interfaces;
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
    public static class SellerCreateDeal
    {
        public class Command : BaseCommand, IRequest<CommandResult>
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string SellerEmail { get; set; }

            public string SellerCountryCode { get; set; }
            public string SellerLocalPhoneNumber { get; set; }
            public string CurrencyCode { get; set; }
            public decimal SellerChargePercentage { get; set; }

            public List<DealItemModel> Items { get; set; }

            public string BuyerUrl { get; set; }
            public string SellerVerificationUrl { get; set; }

            public class DealItemModel
            {
                public decimal Amount { get; set; }
                public int AvailableQuantity { get; set; }
                public string Description { get; set; }
            }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly IPaymentInviteDbContext _context;
                private readonly IIdentityService _identityService;
                private readonly IEventBus _eventBus;
                private readonly HttpClient _httpClient;
                private readonly ConfigOptions _configOptions;

                public Handler(IPaymentInviteDbContext context, IIdentityService identityService, IEventBus eventBus, IHttpClientFactory httpClientFactory, ConfigOptions configOptions)
                {
                    _context = context;
                    _identityService = identityService;
                    _eventBus = eventBus;
                    _httpClient = httpClientFactory.CreateClient();
                    _configOptions = configOptions;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var currency = await _context.Currencies.SingleOrDefaultAsync(x => x.Code == request.CurrencyCode && x.IsActive);

                    if (currency == null)
                    {
                        result.Errors.Add(("CurrencyCode", "invalid currency code"));
                        return result;
                    }

                    var deal = new Deal
                    {
                        Title = request.Title,
                        Description = request.Description,
                        SellerEmail = request.SellerEmail,
                        SellerPhone = new PhoneNumber(request.SellerCountryCode, request.SellerLocalPhoneNumber),
                        SellerChargePercentage = request.SellerChargePercentage,
                        CurrencyId = currency.Id,
                        SellerVerificationCode = _identityService.IsAuthenticated ? null : KeyGenerator.GetUniqueKey(5),
                        Status = DealStatus.Inactive
                    };

                    deal.BuyerLink = $"{request.BuyerUrl}?dealid={deal.Id}";

                    var dealItems = request.Items.Select(x => new DealItem
                    {
                        Amount = x.Amount,
                        Quantity = x.AvailableQuantity,
                        Description = x.Description
                    }).ToList();

                    deal.Items.AddRange(dealItems);

                    if (_identityService.IsAuthenticated)
                    {
                        var sellerAccountGuid = _identityService.AccountId.Value;
                        var seller = await _context.Traders.FirstOrDefaultAsync(x => x.AccountId == sellerAccountGuid);

                        if (seller is null)
                        {
                            seller = new Trader
                            {
                                EmailAddress = _identityService.Email,
                                AccountId = sellerAccountGuid
                            };

                            _context.Traders.Add(seller);
                        }

                        deal.SellerId = seller.Id;
                        deal.IsVerified = true;
                        deal.Status = DealStatus.Active;
                        result.IsVerified = true;
                    }

                    _context.Deals.Add(deal);
                    await _context.SaveChangesAsync(cancellationToken);

                    if (_identityService.IsAuthenticated)
                    {
                        _eventBus.Publish(new DealCreatedEvent(
                              deal.Id.ToString(),
                              request.SellerEmail,
                              deal.SellerPhone.ToString(),
                              currency.Code,
                              deal.SellerChargePercentage,
                              deal.BuyerLink,
                              request.TenantId
                        ));
                    }

                    if (!_identityService.IsAuthenticated)
                    {
                        var command = new { deal.SellerEmail, Code = deal.SellerVerificationCode, SellerPhone = deal.SellerPhone.ToString() };
                        var url = UrlConfig.NotificationOperations.SendVerificationCode(_configOptions.NotificationUrl);

                        var commandContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
                        var response = await _httpClient.PostAsync(url, commandContent).ConfigureAwait(false);
                    }

                    result.DealId = deal.Id;

                    return result;
                }
            }
        }

        public class CommandResult : BaseResult
        {
            public Guid DealId { get; set; }
            public bool IsVerified { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
                RuleFor(x => x.SellerEmail).NotEmpty().EmailAddress();
                RuleFor(x => x.CurrencyCode).NotEmpty();
                RuleFor(x => x.SellerChargePercentage).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
                RuleFor(x => x.Items).NotEmpty();
                RuleFor(x => x.BuyerUrl).NotEmpty();

                RuleForEach(x => x.Items).ChildRules(item =>
                {
                    item.RuleFor(x => x.Description).NotEmpty();
                    item.RuleFor(x => x.Amount).GreaterThan(0);
                    item.RuleFor(x => x.AvailableQuantity).GreaterThanOrEqualTo(1);
                });
            }
        }
    }
}