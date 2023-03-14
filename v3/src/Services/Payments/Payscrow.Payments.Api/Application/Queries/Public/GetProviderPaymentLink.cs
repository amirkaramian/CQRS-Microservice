using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payscrow.Payments.Api.Application.Services.Flutterwave;
using Payscrow.Payments.Api.Data;
using Payscrow.Payments.Api.Domain.Enumerations;
using Payscrow.Payments.Api.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Application.Queries.Public
{
    public static class GetProviderPaymentLink
    {
        public class Query : BaseQuery<QueryResult>
        {
            public Guid? PaymentMethodId { get; set; }

            //public Guid? TransactionRef { get; set; }
            //public decimal Amount { get; set; }
            //public string CurrencyCode { get; set; }
            //public string Name { get; set; }
            //public string EmailAddress { get; set; }
            //public string PhoneNumber { get; set; }
            public string ReturnUrl { get; set; }

            public Guid? PaymentId { get; set; }

            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly PaymentsDbContext _context;
                private readonly FlutterwaveProviderService _flutterwaveProviderService;
                private readonly ILogger _logger;

                public Handler(PaymentsDbContext context, FlutterwaveProviderService flutterwaveProviderService, ILogger<Handler> logger)
                {
                    _context = context;
                    _flutterwaveProviderService = flutterwaveProviderService;
                    _logger = logger;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    var paymentMethod = await _context.PaymentMethods.FindAsync(request.PaymentMethodId);

                    if (paymentMethod == null)
                    {
                        throw new Exception("Payment method not found");
                    }

                    //var currency = await _context.Currencies.SingleOrDefaultAsync(x => x.Code == request.CurrencyCode);

                    //if(currency == null)
                    //{
                    //    throw new Exception("Currency not found");
                    //}

                    //var payment = new Payment {
                    //    Amount = request.Amount,
                    //    PaymentMethodId = paymentMethod.Id,
                    //    TransactionGuid = request.TransactionRef.Value,
                    //    CurrencyId = currency.Id,
                    //    EmailAddress = request.EmailAddress,
                    //    PhoneNumber = request.PhoneNumber
                    //};

                    var payment = await _context.Payments.Include(x => x.Currency).SingleOrDefaultAsync(x => x.Id == request.PaymentId);

                    switch (paymentMethod.Provider)
                    {
                        case PaymentMethodProvider.Flutterwave:

                            payment.PaymentMethodId = paymentMethod.Id;

                            var response = await _flutterwaveProviderService.InitiateStandardPayment(
                                    payment.TransactionGuid.ToString(), payment.Amount.ToString(), request.ReturnUrl, payment.EmailAddress,
                                    payment.PhoneNumber, payment.Name, currency: payment.Currency?.Code
                                );

                            result.Link = response.Data?.Link?.ToString();

                            _logger.LogInformation("Retrived Payment Link from Flutterwave: {PaymentLink}", result.Link);

                            break;

                        case PaymentMethodProvider.Paystack:
                            break;

                        default:
                            break;
                    }

                    await _context.SaveChangesAsync();

                    //if(!await _context.Payments.AnyAsync(x => x.TransactionGuid == payment.TransactionGuid))
                    //{
                    //    _context.Payments.Add(payment);
                    //    await _context.SaveChangesAsync();
                    //}

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public string Link { get; set; }
        }
    }
}