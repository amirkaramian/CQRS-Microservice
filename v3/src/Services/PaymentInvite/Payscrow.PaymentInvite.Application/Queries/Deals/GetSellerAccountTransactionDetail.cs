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
using Payscrow.PaymentInvite.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.Queries.Deals
{
    public static class GetSellerAccountTransactionDetail
    {
        public class Query : IRequest<QueryResult>
        {
            public Guid TransactionId { get; set; }


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

                    var query = (from t in _context.Transactions.Where(x => x.Id == request.TransactionId)
                                 join d in _context.Deals
                                 on t.DealId equals d.Id
                                 join tr in _context.Traders.Where(x => x.AccountId == accountGuid)
                                 on d.SellerId equals tr.Id
                                 select t);

                    var transaction = await query
                        .Include(x => x.Buyer)
                        .Include(x => x.Currency)
                        .Include(x => x.Deal)
                        .FirstOrDefaultAsync();

                    if(transaction != null)
                    {
                        result.Transaction = _mapper.Map<TransactionModel>(transaction);

                        var transactionItems = await _context.TransactionItems
                            .Where(x => x.TransactionId == transaction.Id)
                            .ProjectTo<TransactionItemModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();

                        result.TransactionItems.AddRange(transactionItems);

                        var transactionStatusLogs = await _context.TransactionStatusLogs
                            .Where(x => x.TransactionId == transaction.Id)
                            .ProjectTo<TransactionStatusLogModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();

                        result.TransactionStatusLogs.AddRange(transactionStatusLogs);
                    }                    

                    return result;
                }
            }
        }

        public class QueryResult
        {

            public TransactionModel Transaction { get; set; }
            public List<TransactionItemModel> TransactionItems { get; set; } = new List<TransactionItemModel>();
            public List<TransactionStatusLogModel> TransactionStatusLogs { get; set; } = new List<TransactionStatusLogModel>();
        }

        public class TransactionModel : IMapFrom<Transaction>
        {
            public Guid Id { get; set; }
            public string Number { get; set; }
            public int StatusId { get; set; }
            public string Status => Enumeration.FromValue<TransactionStatus>(StatusId)?.Name;

            public PaymentStatus PaymentStatus { get; set; }

            public bool InEscrow { get; set; }


            public string BuyerEmail { get; set; }
            public string BuyerPhoneLocalNumber { get; set; }
            public string BuyerPhoneCountryCode { get; set; }

            public Guid? BuyerId { get; set; }
            public string BuyerName { get; set; }
            public string BuyerEmailAddress { get; set; }
            public string BuyerContactAddress { get; set; }

            public Address DeliveryAddress { get; set; }


            public Guid DealId { get; set; }
            public string DealTitle { get; set; }
            public string DealDescription { get; set; }


            public Guid CurrencyId { get; set; }
            public string CurrencyCode { get; set; }
            public string CurrencySymbol { get; set; }


            public decimal TotalAmount { get; set; }
            public decimal SellerChargeAmount { get; set; }
            public decimal BuyerChargeAmount { get; set; }


            public DateTime CreateUtc { get; set; }

            //public List<Note> Notes { get; private set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Transaction, TransactionModel>()
                    .ForMember(x => x.Number, src => src.MapFrom(x => x.Number.ToString("D8")));
            }
        }

        public class TransactionItemModel : IMapFrom<TransactionItem>
        {
            public Guid Id { get; set; }
            public int Quantity { get; set; }
            public string ImageFileName { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Amount { get; set; }
            public decimal Total => Quantity * Amount;

            public void Mapping(Profile profile)
            {
                profile.CreateMap<TransactionItem, TransactionItemModel>();
            }
        }

        public class TransactionStatusLogModel : IMapFrom<TransactionStatusLog>
        {
            public Guid Id { get; set; }
            public int TransactionStatusId { get; set; }
            public string TransactionStatus => Enumeration.FromValue<TransactionStatus>(TransactionStatusId)?.DisplayName;
            public DateTime CreateUtc { get; set; }

            public string Comment { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<TransactionStatusLog, TransactionStatusLogModel>();
            }
        }
    }
}
