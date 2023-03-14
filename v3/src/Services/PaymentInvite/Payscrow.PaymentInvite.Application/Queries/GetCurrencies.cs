using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.PaymentInvite.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.Queries
{
    public static class GetCurrencies
    {
        public class Query : IRequest<QueryResult>
        {


            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly IPaymentInviteDbContext _context;

                public Handler(IPaymentInviteDbContext context)
                {
                    _context = context;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    var currencies = await _context.Currencies.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Order)
                        .Select(x => new QueryResult.CurrencyModel { Id = x.Id, Code = x.Code, Name = x.Name, Symbol = x.Symbol }).ToListAsync();

                    result.Currencies.AddRange(currencies);

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public List<CurrencyModel> Currencies { get; set; } = new List<CurrencyModel>();

            public class CurrencyModel
            {
                public Guid Id { get; set; }
                public string Name { get; set; }
                public string Symbol { get; set; }
                public string Code { get; set; }
            }
        }
    }
}
