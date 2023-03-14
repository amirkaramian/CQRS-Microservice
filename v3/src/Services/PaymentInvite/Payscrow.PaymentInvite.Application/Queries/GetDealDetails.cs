using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.PaymentInvite.Application.Common.Exceptions;
using Payscrow.PaymentInvite.Application.Common.Mappings;
using Payscrow.PaymentInvite.Application.Interfaces;
using Payscrow.PaymentInvite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.Queries
{
    public static class GetDealDetails
    {
        public class Query : IRequest<QueryResult>
        {
            public Guid? DealId { get; set; }

            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly IPaymentInviteDbContext _context;
                private readonly IMapper _mapper;
                private readonly IChargeCalculatorService _chargeCalculatorService;

                public Handler(IPaymentInviteDbContext context, IMapper mapper, IChargeCalculatorService chargeCalculatorService)
                {
                    _context = context;
                    _mapper = mapper;
                    _chargeCalculatorService = chargeCalculatorService;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var deal = await _context.Deals.Include(x => x.Currency).FirstOrDefaultAsync(x => x.Id == request.DealId);

                    if (deal == null)
                    {
                        throw new NotFoundException(nameof(Deal), request.DealId);
                    }

                    var result = _mapper.Map<QueryResult>(deal);

                    var items = await _context.DealItems.Where(x => x.DealId == request.DealId)
                        .Select(x => new QueryResult.DealItemModel
                        {
                            Id = x.Id,
                            Amount = x.Amount,
                            Description = x.Description,
                            Quantity = x.Quantity,
                            Name = x.Name
                        })
                        .ToListAsync();                    

                    result.Items.AddRange(items);

                    var buyerCharge = await _chargeCalculatorService.GetChargeAsync(result.TotalAmount, 100 - deal.SellerChargePercentage, deal.Currency?.Code);

                    result.BuyerTransactionCharge = buyerCharge;

                    return result;
                }
            }
        }

        public class QueryResult : IMapFrom<Deal>
        {
            public string SellerEmail { get; set; }
            //public PhoneNumber SellerPhone { get; set; }
            public string SellerPhone { get; set; }
            public decimal SellerChargePercentage { get; set; }
            public string SellerVerificationCode { get; set; }

            public string BuyerLink { get; set; }

            public bool IsVerified { get; set; }

            public Guid CurrencyId { get; set; }
            public string CurrencyCode { get; set; }

            public Guid? SellerId { get; set; }
            public string SellerName { get; set; }

            public decimal BuyerTransactionCharge { get; set; }
            public decimal SellerTransactionCharge { get; set; }

            public List<DealItemModel> Items { get; set; } = new List<DealItemModel>();

            public decimal TotalAmount => Items.Sum(x => x.Quantity * x.Amount);

            public class DealItemModel
            {
                public Guid Id { get; set; }
                public string Name { get; set; }
                public decimal Amount { get; set; }
                public int Quantity { get; set; }
                public string Description { get; set; }
            }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Deal, QueryResult>()
                    .ForMember(x => x.Items, src => src.Ignore());
            }
        }
    }
}
