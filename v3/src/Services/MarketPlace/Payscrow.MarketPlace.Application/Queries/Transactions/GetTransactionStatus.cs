using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.MarketPlace.Application.Data;
using Payscrow.MarketPlace.Application.Domain;
using Payscrow.MarketPlace.Application.Domain.Enumerations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.Queries.Transactions
{
    public static class GetTransactionStatus
    {
        public class Query : BaseQuery<QueryResult>
        {
            public Guid TransactionId { get; set; }
            public Guid BrokerAccountId { get; set; }

            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly MarketPlaceDbContext _context;

                public Handler(MarketPlaceDbContext context)
                {
                    _context = context;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    var transaction = await _context.Transactions.ForTenant(request.TenantId)
                        .FirstOrDefaultAsync(x => x.Id == request.TransactionId && x.BrokerAccountId == request.BrokerAccountId, cancellationToken);

                    if (transaction != null)
                    {
                        var status = Enumeration.FromValue<TransactionStatus>(transaction.StatusId);

                        result.TransactionId = transaction.Id;
                        result.Status = status.DisplayName;
                        result.StatusId = status.Id;
                        result.InEscrow = transaction.InEscrow;
                        result.InDispute = transaction.InDispute;
                    }

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public Guid? TransactionId { get; set; }
            public string Status { get; set; }
            public bool? InEscrow { get; set; }
            public bool? InDispute { get; set; }
            public int? StatusId { get; set; }
        }
    }
}