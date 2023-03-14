using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.Payments.Api.Application.Extensions;
using Payscrow.Payments.Api.Data;
using Payscrow.Payments.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Application.Queries.Public
{
    public static class GetAvailablePaymentMethods
    {
        public class Query : BaseQuery<QueryResult>
        {
            public string CurrencyCode { get; set; }
            public Guid? AccountId { get; set; }

            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly PaymentsDbContext _context;
                private readonly AppSettings _appSettings;

                public Handler(PaymentsDbContext context, AppSettings appSettings)
                {
                    _context = context;
                    _appSettings = appSettings;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    IQueryable<PaymentMethod> query;

                    if (request.AccountId.HasValue && request.AccountId.Value != Guid.Empty
                        && await _context.AccountPaymentMethods.AnyAsync(x => x.AccountId == request.AccountId))
                    {
                        query = (from c in _context.Currencies
                                 join cpm in _context.PaymentMethodCurrencies on c.Id equals cpm.CurrencyId
                                 join pm in _context.PaymentMethods on cpm.PaymentMethodId equals pm.Id
                                 join apm in _context.AccountPaymentMethods on pm.Id equals apm.PaymentMethodId
                                 where pm.IsActive && c.Code == request.CurrencyCode && apm.AccountId == request.AccountId
                                 && pm.TenantId == request.TenantId
                                 select pm
                                 ).AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        query = (from c in _context.Currencies
                                 join cpm in _context.PaymentMethodCurrencies
                                 on c.Id equals cpm.CurrencyId
                                 join pm in _context.PaymentMethods
                                 on cpm.PaymentMethodId equals pm.Id
                                 where pm.IsActive && c.Code == request.CurrencyCode && pm.TenantId == request.TenantId
                                 select pm
                                ).AsNoTracking().AsQueryable();
                    }

                    var paymentMethods = await query.ToListAsync();

                    foreach (var paymentMethod in paymentMethods)
                    {
                        paymentMethod.SetLogoUri(_appSettings.Paths.Logos);
                    }

                    var paymentMethodModels = paymentMethods.Select(x => new QueryResult.PaymentMethodModel
                    {
                        Id = x.Id.ToString(),
                        Name = x.Name,
                        LogoFileName = x.LogoFileName,
                        LogoUri = x.LogoUri
                    }).ToList();

                    result.PaymentMethods.AddRange(paymentMethodModels);

                    //result.PaymentMethods.AddRange(new List<QueryResult.PaymentMethodModel> {
                    //    new QueryResult.PaymentMethodModel{
                    //        Name = "Rave Pay"
                    //    }
                    //});

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public List<PaymentMethodModel> PaymentMethods { get; set; } = new List<PaymentMethodModel>();

            public class PaymentMethodModel
            {
                public string Id { get; set; }
                public string Name { get; set; }
                public string LogoFileName { get; set; }
                public string RedirectUrl { get; set; }
                public string LogoUri { get; set; }
            }
        }
    }
}