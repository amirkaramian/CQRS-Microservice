using AutoMapper;
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
    public static class GetTransactionDetail
    {
        public class Query : BaseQuery<QueryResult>
        {
            public Guid TransactionId { get; set; }
            public Guid AccountId { get; set; }

            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly IMarketPlaceDbContext _context;
                private readonly IMapper _mapper;

                public Handler(IMarketPlaceDbContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    var transaction = await _context.Transactions.ForTenant(request.TenantId)
                        .Include(x => x.Items).Include(x => x.SettlementAccounts).Include(x => x.TransactionStatusLogs).Include(x => x.Currency)
                        .Where(x => x.Id == request.TransactionId && (x.CustomerAccountId == request.AccountId
                        || x.MerchantAccountId == request.AccountId || x.BrokerAccountId == request.AccountId))
                        .FirstOrDefaultAsync(cancellationToken);

                    if (transaction is null) return result;

                    result.Transaction = _mapper.Map<QueryResult.TransactionDto>(transaction);

                    result.Items = _mapper.Map<List<Item>, List<QueryResult.TransactionItemDto>>(transaction.Items.ToList());

                    result.SettlementAccounts = _mapper.Map<List<SettlementAccount>, List<QueryResult.SettlementAccountDto>>(transaction.SettlementAccounts.ToList());

                    result.StatusLogs = _mapper.Map<List<TransactionStatusLog>, List<QueryResult.TransactionStatusLogDto>>(transaction.TransactionStatusLogs.ToList());

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public TransactionDto Transaction { get; set; }
            public List<TransactionItemDto> Items { get; set; }
            public List<SettlementAccountDto> SettlementAccounts { get; set; }
            public List<TransactionStatusLogDto> StatusLogs { get; set; }

            public class TransactionDto : IMapFrom<Transaction>
            {
                public string Number { get; set; }
                public long FriendlyNumber { get; set; }

                public Guid BrokerAccountId { get; set; }
                public string BrokerTransactionReference { get; set; }
                public string BrokerName { get; set; }
                public decimal BrokerFee { get; set; }

                public Guid MerchantAccountId { get; set; }
                public string MerchantName { get; set; }
                public string MerchantEmailAddress { get; set; }
                public string MerchantPhone { get; set; }
                public decimal MerchantCharge { get; set; }

                public Guid? CustomerAccountId { get; set; }
                public string CustomerName { get; set; }
                public string CustomerEmailAddress { get; set; }
                public string CustomerPhone { get; set; }
                public decimal CustomerCharge { get; set; }
                public decimal Total => GrandTotalPayable - CustomerCharge;
                public decimal GrandTotalPayable { get; set; }

                public string CurrencyCode { get; set; }

                public int StatusId { get; set; }
                public TransactionStatus Status => Enumeration.FromValue<TransactionStatus>(StatusId);
                public bool InEscrow { get; set; }
                public bool InDispute { get; set; }
                public string EscrowCode { get; set; }

                public TransactionPaymentStatus PaymentStatus { get; set; }

                public DateTime CreateUtc { get; set; }
            }

            public class TransactionItemDto : IMapFrom<Item>
            {
                public Guid Id { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
                public int Quantity { get; set; }
                public decimal Price { get; set; }
                public decimal Total => Quantity * Price;
            }

            public class SettlementAccountDto : IMapFrom<SettlementAccount>
            {
                public Guid Id { get; set; }
                public string BankCode { get; set; }
                public string AccountNumber { get; set; }
                public string AccountName { get; set; }
                public decimal Amount { get; set; }
            }

            public class TransactionStatusLogDto : IMapFrom<TransactionStatusLog>
            {
                public int StatusId { get; set; }
                public TransactionStatus Status => Enumeration.FromValue<TransactionStatus>(StatusId);

                public bool InDispute { get; set; }
                public bool InEscrow { get; set; }
                public string Note { get; set; }

                public DateTime CreateUtc { get; set; }
            }
        }
    }
}