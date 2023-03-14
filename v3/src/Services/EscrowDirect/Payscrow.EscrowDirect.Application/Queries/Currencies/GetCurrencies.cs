using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.Core.Interfaces;
using Payscrow.EscrowDirect.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.EscrowDirect.Application.Queries.Currencies
{
    public static class GetCurrencies
    {
        public class Query : IRequest<QueryResult>
        {

            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly ITenantService _tenantService;
                private readonly IEscrowDirectDbContext _context; 

                public Handler(ITenantService tenantService, IEscrowDirectDbContext context)
                {
                    _tenantService = tenantService;
                    _context = context;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    var tenantId = await _tenantService.GetTenantIdAsync();

                    var currencies = await _context.Currencies
                        .Where(x => x.TenantId == tenantId && x.IsActive)
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
