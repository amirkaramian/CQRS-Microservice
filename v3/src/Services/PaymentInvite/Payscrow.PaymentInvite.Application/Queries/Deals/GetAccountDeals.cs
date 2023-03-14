using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.Core.Interfaces;
using Payscrow.PaymentInvite.Application.Common.Mappings;
using Payscrow.PaymentInvite.Application.Interfaces;
using Payscrow.PaymentInvite.Domain.Enumerations;
using Payscrow.PaymentInvite.Domain.Models;
using Payscrow.PaymentInvite.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.Queries
{
    public static class GetAccountDeals
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

                    if (!accountGuid.HasValue) return result;

                    var query = (from d in _context.Deals
                                 join s in _context.Traders.Where(x => x.AccountId == accountGuid)
                                 on d.SellerId equals s.Id
                                 join c in _context.Currencies.Where(x => x.Code == request.CurrencyCode)
                                 on d.CurrencyId equals c.Id
                                 select d)
                                 .AsNoTracking()
                                 .AsQueryable();

                    result.RecordsTotal = await query.CountAsync();

                    if (request.Search != null)
                    {
                        if (!string.IsNullOrWhiteSpace(request.Search.Title))
                        {
                            query = query.Where(x => x.Title.Contains(request.Search.Title));
                        }

                        if (request.Search.Status.HasValue)
                        {
                            query = query.Where(x => x.Status == request.Search.Status.Value);
                        }
                    }

                    result.RecordsFiltered = await query.CountAsync();

                    var deals = await query.ProjectTo<DealModel>(_mapper.ConfigurationProvider)
                        .ToListAsync();

                    result.Data.AddRange(deals);

                    return result;
                }
            }

            public class SearchParameter
            {
                public string Title { get; set; }
                public DealStatus? Status { get; set; }
                
            }
        }

        public class QueryResult : BaseDataQueryResult<DealModel>
        {           
        }

        public class DealModel : IMapFrom<Deal>
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
           
            public decimal SellerChargePercentage { get; set; }
            public string SellerVerificationCode { get; set; }

            public string BuyerLink { get; set; }

            public bool IsVerified { get; set; }

            public Guid CurrencyId { get; set; }


            public DealStatus Status { get; set; }
            public DateTime CreateUtc { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Deal, DealModel>();
            }
        }
    }    
}
