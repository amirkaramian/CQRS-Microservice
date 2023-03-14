using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.Core.Bus;
using Payscrow.PaymentInvite.Application.IntegrationEvents;
using Payscrow.PaymentInvite.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.Commands
{
    public static class SellerConfirmAnonymousDeal
    {
        public class Command : BaseCommand, IRequest<CommandResult>
        {
            public string DealId { get; set; }
            public string Code { get; set; }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly IPaymentInviteDbContext _context;
                private readonly IEventBus _eventBus;

                public Handler(IPaymentInviteDbContext context, IEventBus eventBus)
                {
                    _context = context;
                    _eventBus = eventBus;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var id = Guid.Parse(request.DealId);

                    var deal = await _context.Deals.Include(x => x.Currency).FirstOrDefaultAsync(x => x.Id == id);

                    if (deal == null)
                    {
                        result.Errors.Add(("DealId", "Invalid deal ID"));
                        return result;
                    }

                    if (!deal.SellerVerificationCode.Equals(request.Code, StringComparison.OrdinalIgnoreCase))
                    {
                        result.Errors.Add(("Code", "Incorrect verfication code"));
                        return result;
                    }

                    if (!deal.IsVerified)
                    {
                        deal.IsVerified = true;

                        var seller = await _context.Traders.Where(x => x.EmailAddress == deal.SellerEmail).SingleOrDefaultAsync();

                        if (seller != null)
                        {
                            seller.Deals.Add(deal);
                        }

                        await _context.SaveChangesAsync(cancellationToken);

                        _eventBus.Publish(
                            new DealCreatedEvent(
                                deal.Id.ToString(),
                                deal.SellerEmail,
                                deal.SellerPhone?.ToString(),
                                deal.Currency?.Code,
                                deal.SellerChargePercentage,
                                deal.BuyerLink,
                                request.TenantId
                            ));
                    }

                    result.DealBuyerUrl = deal.BuyerLink;

                    return result;
                }
            }
        }

        public class CommandResult : BaseResult
        {
            public string DealBuyerUrl { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DealId).NotEmpty().Must(ValidateInviteId).WithMessage("invalid deal Id");
                RuleFor(x => x.Code).NotEmpty().MaximumLength(10);
            }

            private bool ValidateInviteId(string inviteId)
            {
                return Guid.TryParse(inviteId, out var result);
            }
        }
    }
}