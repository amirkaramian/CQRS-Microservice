using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.Core.Interfaces;
using Payscrow.PaymentInvite.Application.Common.Mappings;
using Payscrow.PaymentInvite.Application.Interfaces;
using Payscrow.PaymentInvite.Domain.Enumerations;
using Payscrow.PaymentInvite.Domain.Models;
using Payscrow.PaymentInvite.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.Queries.Deals
{
    public static class GetAccountTransactions
    {
        public class Query : IRequest<QueryResult>
        {

            public int Page { get; set; }
            public int PageSize { get; set; }
            public string CurrencyCode { get; set; }
            public SearchParameter Search { get; set; }


            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly IPaymentInviteDbContext _context;
                private readonly IMapper _mapper;
                private readonly IIdentityService _identityService;

                public Handler(IPaymentInviteDbContext context, IMapper mapper, IIdentityService identityService)
                {
                    _context = context;
                    _mapper = mapper;
                    _identityService = identityService;
                }


                async public Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    var accountGuid = _identityService.AccountId;
                    var tenantId = _identityService.TenantId;

                    if (!accountGuid.HasValue) return result;

                    var query = (from t in _context.Transactions.Include(x => x.Deal).ThenInclude(x => x.Currency)
                                 join d in _context.Deals
                                 on t.DealId equals d.Id
                                 join s in _context.Traders.Where(x => x.AccountId == accountGuid)
                                 on d.SellerId equals s.Id
                                 join c in _context.Currencies.Where(x => x.Code == request.CurrencyCode)
                                 on d.CurrencyId equals c.Id
                                 where t.TenantId == tenantId
                                 select t)
                                .AsNoTracking()
                                .AsQueryable();

                    result.RecordsTotal = await query.CountAsync();

                    if (request.Search != null)
                    {
                        if (request.Search.StatusId.HasValue)
                        {
                            query = query.Where(x => x.StatusId == request.Search.StatusId);
                        }

                        if (request.Search.PaymentStatus.HasValue)
                        {
                            query = query.Where(x => x.PaymentStatus == request.Search.PaymentStatus);
                        }
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
                public PaymentStatus? PaymentStatus { get; set; }
            }
        }

        public class QueryResult : BaseDataQueryResult<TransactionModel>
        {
        }

        public class TransactionModel : IMapFrom<Transaction>
        {
            public Guid Id { get; set; }

            public int StatusId { get; set; }
            public string StatusName => Enumeration.FromValue<TransactionStatus>(StatusId)?.Name;

            public PaymentStatus PaymentStatus { get; set; }

            public bool InEscrow { get; set; }


            public string BuyerEmail { get; set; }
            public string BuyerPhoneLocalNumber { get; set; }


            public Guid? BuyerId { get; set; }


            public Guid DealId { get; set; }
            public string DealTitle { get; set; }
            public string DealDescription { get; set; }
            public string DealCurrencySymbol { get; set; }


            public decimal TotalAmount { get; set; }
            public decimal SellerChargeAmount { get; set; }
            public decimal BuyerChargeAmount { get; set; }

            public DateTime CreateUtc { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Transaction, TransactionModel>();
            }
        }
    }
}
