using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.MarketPlace.Application.Common.Mapping;
using Payscrow.MarketPlace.Application.Domain;
using Payscrow.MarketPlace.Application.Domain.Entities;
using Payscrow.MarketPlace.Application.Domain.Enumerations;
using Payscrow.MarketPlace.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.Queries.Transactions
{
    public static class GetPendingTransactionDetail
    {
        public class Query : BaseQuery<QueryResult>
        {
            public Guid TransactionId { get; set; }


            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly IMarketPlaceDbContext _context;
                private readonly IMapper _mapper;

                public Handler(IMarketPlaceDbContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                async public Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult
                    {
                        Transaction = await _context.Transactions.ForTenant(request.TenantId)
                                        .Where(x => x.Id == request.TransactionId && x.StatusId == TransactionStatus.Pending.Id)
                                        .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync()
                    };

                    if (result.Transaction != null)
                    {
                        var items = await _context.Items.ForTenant(request.TenantId)
                        .Where(x => x.TransactionId == request.TransactionId)
                        .ProjectToListAsync<TransactionItemDto>(_mapper.ConfigurationProvider);

                        result.Items.AddRange(items);
                    }

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public TransactionDto Transaction { get; set; }
            public List<TransactionItemDto> Items { get; set; } = new List<TransactionItemDto>();
        }

        public class TransactionDto : BaseDto, IMapFrom<Transaction>
        {
            public string Number { get; set; }

            public Guid BrokerAccountId { get; set; }
            public string BrokerName { get; set; }


            public Guid MerchantAccountId { get; set; }
            public string MerchantName { get; set; }
            public string MerchantEmailAddress { get; set; }
            public decimal MerchantCharge { get; set; }


            public Guid? CustomerAccountId { get; set; }
            public string CustomerName { get; set; }
            public string CustomerEmailAddress { get; set; }
            public decimal CustomerCharge { get; set; }


            public decimal GrandTotalPayable { get; set; }

            public Guid CurrencyId { get; set; }
            public string CurrencyName { get; set; }
            public string CurrencyCode { get; set; }


            //public int StatusId { get; set; }
            public TransactionStatus Status { get; set; }


            public TransactionPaymentStatus PaymentStatus { get; set; }
            public string PaymentMethod { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Transaction, TransactionDto>()
                    .ForMember(x => x.Status, src => src.MapFrom(x => Enumeration.FromValue<TransactionStatus>(x.StatusId)));
            }
        }

        public class TransactionItemDto : BaseDto, IMapFrom<Item>
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
}
