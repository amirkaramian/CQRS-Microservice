using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.MarketPlace.Application.Common.Mapping;
using Payscrow.MarketPlace.Application.Data;
using Payscrow.MarketPlace.Application.Domain;
using Payscrow.MarketPlace.Application.Domain.Entities;
using Payscrow.MarketPlace.Application.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.Queries.Transactions
{
    public static class GetTransactions
    {
        public class Query : BaseQuery<QueryResult>
        {
            public Guid AccountId { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
            public string CurrencyCode { get; set; }
            public SearchParameter Search { get; set; }

            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly MarketPlaceDbContext _context;
                private readonly IMapper _mapper;

                public Handler(MarketPlaceDbContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    var query = (from t in _context.Transactions.Include(x => x.Currency)
                                 join c in _context.Currencies.Where(x => x.Code == request.CurrencyCode)
                                 on t.CurrencyId equals c.Id
                                 where (t.MerchantAccountId == request.AccountId || t.CustomerAccountId == request.AccountId || t.BrokerAccountId == request.AccountId)
                                 && t.TenantId == request.TenantId && c.TenantId == request.TenantId
                                 select t)
                                .AsNoTracking()
                                .OrderByDescending(x => x.CreateUtc)
                                .AsQueryable();

                    result.RecordsTotal = await query.CountAsync();

                    if (request.Search != null)
                    {
                        if (request.Search.StatusId.HasValue)
                        {
                            query = query.Where(x => x.StatusId == request.Search.StatusId);
                        }

                        //if (request.Search.PaymentStatus.HasValue)
                        //{
                        //    query = query.Where(x => x.PaymentStatus == request.Search.PaymentStatus);
                        //}
                    }

                    result.RecordsFiltered = await query.CountAsync();

                    var transactions = await query.ToListAsync();
                    //var transactions = await query
                    // .ProjectTo<TransactionModel>(_mapper.ConfigurationProvider)
                    //.ToListAsync();

                    //result.Data.AddRange(transactions);

                    result.Data.AddRange(_mapper.Map<List<Transaction>, List<TransactionModel>>(transactions));

                    return result;
                }
            }

            public class SearchParameter
            {
                public int? StatusId { get; set; }
                //public PaymentStatus? PaymentStatus { get; set; }
            }
        }

        public class QueryResult : BaseDataQueryResult<TransactionModel>
        {
        }

        public class TransactionModel : IMapFrom<Transaction>
        {
            public Guid Id { get; set; }

            public string Number { get; set; }

            public Guid BrokerAccountId { get; set; }
            public string BrokerTransactionReference { get; set; }
            public string BrokerName { get; set; }
            public decimal BrokerFee { get; set; }

            public Guid MerchantAccountId { get; set; }
            public string MerchantName { get; set; }
            public string MerchantEmailAddress { get; set; }
            public decimal MerchantCharge { get; set; }

            public Guid? CustomerAccountId { get; set; }
            public string CustomerName { get; set; }
            public string CustomerEmailAddress { get; set; }
            public decimal CustomerCharge { get; set; }

            public decimal GrandTotalPayable { get; set; }

            public int StatusId { get; set; }
            public string StatusName => Enumeration.FromValue<TransactionStatus>(StatusId)?.DisplayName;
            public bool InEscrow { get; set; }
            public bool InDispute { get; set; }
            public string EscrowCode { get; set; }

            public TransactionPaymentStatus PaymentStatus { get; set; }
            public string PaymentMethod { get; set; }

            public Guid CreateUserId { get; set; }
            public DateTime CreateUtc { get; set; }
            public Guid? UpdateUserId { get; set; }
            public DateTime? UpdateUtc { get; set; }

            //public PaymentStatus PaymentStatus { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Transaction, TransactionModel>();
            }
        }
    }
}