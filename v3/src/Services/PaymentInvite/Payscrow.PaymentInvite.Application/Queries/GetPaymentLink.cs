using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Payscrow.PaymentInvite.Application.Common;
using Payscrow.PaymentInvite.Application.Common.Exceptions;
using Payscrow.PaymentInvite.Application.Interfaces;
using Payscrow.PaymentInvite.Domain.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.Queries
{
    public static class GetPaymentLink
    {
        public class Query : IRequest<QueryResult>
        {
            public Guid? TransactionId { get; set; }
            public Guid? PaymentMethodId { get; set; }
            public string ReturnUrl { get; set; }


            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly IPaymentInviteDbContext _context;
                private readonly HttpClient _httpClient;
                private readonly ConfigOptions _configOptions;

                public Handler(IPaymentInviteDbContext context, IHttpClientFactory httpClientFactory, ConfigOptions configOptions)
                {
                    _context = context;
                    _httpClient = httpClientFactory.CreateClient();
                    _configOptions = configOptions;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {         
                    var transaction = await _context.Transactions
                        .Include(x => x.Deal)
                        .ThenInclude(x => x.Currency)
                        .SingleOrDefaultAsync(x => x.Id == request.TransactionId);

                    if(transaction == null)
                    {
                        throw new NotFoundException(nameof(Transaction), request.TransactionId);
                    }

                    var plrm = new PaymentLinkRequestModel { 
                        PaymentMethodId = request.PaymentMethodId?.ToString(),
                        TransactionRef = transaction.Id.ToString(),
                        Amount = transaction.TotalAmount,
                        CurrencyCode = transaction.Deal?.Currency?.Code,
                        Name = transaction.BuyerEmail,
                        EmailAddress = transaction.BuyerEmail,
                        PhoneNumber = transaction.BuyerPhone?.ToString(),
                        ReturnUrl = request.ReturnUrl
                    };

                    var queryContent = new StringContent(JsonConvert.SerializeObject(plrm), Encoding.UTF8, "application/json");
                    var requestUrl = UrlConfig.PaymentOperations.RequestPaymentLink(_configOptions.PaymentsUrl);

                    var response = await _httpClient.PostAsync(requestUrl, queryContent);

                    var result = JsonConvert.DeserializeObject<QueryResult>(await response.Content.ReadAsStringAsync());

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public string Link { get; set; }
        }

        public class PaymentLinkRequestModel
        {
            public string PaymentMethodId { get; set; }
            public string TransactionRef { get; set; }
            public decimal Amount { get; set; }
            public string CurrencyCode { get; set; }
            public string Name { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string ReturnUrl { get; set; }
        }
    }
}
