using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.MarketPlace.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.Queries.Currencies
{
    public static class GetCurrencies
    {
        public class Query : BaseQuery<QueryResult>
        {
            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly IMarketPlaceDbContext _context;

                public Handler(IMarketPlaceDbContext context)
                {
                    _context = context;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    var currencies = await _context.Currencies.ForTenant(request.TenantId)
                        .Where(x => x.IsActive)
                        .Select(x => new CurrencyDto { Code = x.Code, Id = x.Id, Name = x.Name })
                        .ToListAsync();

                    result.Currencies.AddRange(currencies);

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public List<CurrencyDto> Currencies { get; set; } = new List<CurrencyDto>();
        }

        public class CurrencyDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
        }
    }
}