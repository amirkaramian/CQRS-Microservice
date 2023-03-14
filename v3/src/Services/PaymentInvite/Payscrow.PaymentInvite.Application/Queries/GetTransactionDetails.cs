using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payscrow.PaymentInvite.Application.Common.Exceptions;
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
    public static class GetTransactionDetails
    {
        public class Query : IRequest<QueryResult>
        {
            public Guid? TransactionId { get; set; }


            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly IPaymentInviteDbContext _context;
                private readonly IMapper _mapper;
                private readonly ILogger _logger;

                public Handler(IPaymentInviteDbContext context, IMapper mapper, ILogger<Handler> logger)
                {
                    _context = context;
                    _mapper = mapper;
                    _logger = logger;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var transaction = await _context.Transactions.Include(x => x.Items)
                        .SingleOrDefaultAsync(x => x.Id == request.TransactionId);

                    if(transaction == null) {
                        _logger.LogError("transaction with ID: {TransactionId}", request.TransactionId);
                        throw new NotFoundException(nameof(Transaction), request.TransactionId);
                    }

                    var result = _mapper.Map<QueryResult>(transaction);

                    var transactionItems = transaction.Items.Select(x => new QueryResult.TransactionItemModel { 
                        Amount = x.Amount,
                        Description = x.Description,
                        Name = x.Name,
                        Quantity = x.Quantity
                    }).ToList();

                    result.Items.AddRange(transactionItems);

                    return result;
                }
            }
        }

        public class QueryResult
        {

            public int StatusId { get; set; }
            //public TransactionStatus Status { get; private set; }

            public PaymentStatus PaymentStatus { get; set; }
            public bool InEscrow { get; set; }


            public string BuyerEmail { get; set; }
            public PhoneNumber BuyerPhone { get; set; }


            public Guid? BuyerId { get; set; }
            //public Trader Buyer { get; set; }


            public Guid DealId { get; set; }
            //public Deal Deal { get; set; }


            public decimal TotalAmount { get; set; }
            public decimal SellerChargeAmount { get; set; }
            public decimal BuyerChargeAmount { get; set; }

            public List<TransactionItemModel> Items { get; set; } = new List<TransactionItemModel>();


            public class TransactionItemModel
            {
                public int Quantity { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
                public decimal Amount { get; set; }
            }
        }
    }
}
