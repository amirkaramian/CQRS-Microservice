using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.Escrow.Application.Common.Mappings;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Domain.Enumerations;
using Payscrow.Escrow.Domain.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.Queries.Business.EscrowTransactions
{
    public static class GetEscrowTransaction
    {
        public class Query : BaseQuery<QueryResult>
        {
            public Guid TransactionGuid { get; set; }
            public Guid AccountGuid { get; set; }

            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly IEscrowDbContext _context;
                private readonly IMapper _mapper;

                public Handler(IEscrowDbContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    var transaction = await (from et in _context.EscrowTransactions.ForTenant(request.TenantId).Include(x => x.Currency)
                                             join eta in _context.EscrowTransactionAccounts on et.Id equals eta.EscrowTransactionId
                                             join a in _context.Accounts on eta.AccountId equals a.Id
                                             where a.AccountGuid == request.AccountGuid && et.TransactionGuid == request.TransactionGuid
                                             select et)
                                           .SingleOrDefaultAsync();

                    if (transaction != null)
                    {
                        result.Transaction = _mapper.Map<TransactionDto>(transaction);
                    }

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public TransactionDto Transaction { get; set; }
        }

        public class TransactionDto : IMapFrom<EscrowTransaction>
        {
            public string TransactionNumber { get; set; }
            public decimal Amount { get; set; }

            public string CurrencyCode { get; set; }
            public Guid TransactionGuid { get; set; }

            public int StatusId { get; set; }
            public EscrowTransactionStatus Status => Enumeration.FromValue<EscrowTransactionStatus>(StatusId);

            public bool InDispute { get; set; }

            public int ServiceTypeId { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<EscrowTransaction, TransactionDto>();
            }
        }
    }
}