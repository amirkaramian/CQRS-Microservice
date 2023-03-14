using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.Core.Bus;
using Payscrow.MarketPlace.Application.Domain.Entities;
using Payscrow.MarketPlace.Application.Domain.Enumerations;
using Payscrow.MarketPlace.Application.IntegrationEvents.Publishing;
using Payscrow.MarketPlace.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.Commands.Business.Transactions
{
    public static class RaiseTransactionDispute
    {
        public class Command : BaseCommand<CommandResult>
        {
            public Guid TransactionId { get; set; }
            public Guid AccountId { get; set; }
            public string Complaint { get; set; }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly IMarketPlaceDbContext _context;
                private readonly IEventBus _eventBus;

                public Handler(IMarketPlaceDbContext context, IEventBus eventBus)
                {
                    _context = context;
                    _eventBus = eventBus;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var transaction = await _context.Transactions.ForTenant(request.TenantId)
                        .FirstOrDefaultAsync(x => x.Id == request.TransactionId
                        && (x.MerchantAccountId == request.AccountId || x.CustomerAccountId == request.AccountId
                        || x.BrokerAccountId == request.AccountId), cancellationToken);

                    if (transaction is null)
                    {
                        result.Errors.Add(("TransactionId", "Transaction could not be found!"));
                        return result;
                    }

                    if (transaction.StatusId == TransactionStatus.Finalized.Id)
                    {
                        result.Errors.Add(("TransactionStatus", "This transaction is already concluded and a dispute cannot be raised!"));
                        return result;
                    }

                    if (transaction.StatusId == TransactionStatus.Pending.Id)
                    {
                        result.Errors.Add(("TransactionStatus", "A dispute cannot be raised for a transaction that has not entered escrow!"));
                        return result;
                    }

                    if (request.AccountId != transaction.MerchantAccountId && request.AccountId != transaction.CustomerAccountId)
                    {
                        result.Errors.Add(("Unauthorized", "A dispute can only be raised by a customer or merchant!"));
                        return result;
                    }

                    if (transaction.InDispute)
                    {
                        result.Errors.Add(("Invalid", "This transaction is already in Dispute!"));
                        return result;
                    }

                    transaction.InDispute = true;
                    transaction.UpdateUtc = DateTime.UtcNow;

                    var statusLog = new TransactionStatusLog
                    {
                        TransactionId = transaction.Id,
                        StatusId = transaction.StatusId,
                        InDispute = transaction.InDispute,
                        InEscrow = transaction.InEscrow,
                        Note = "Transaction was Disputed",
                        TenantId = transaction.TenantId
                    };
                    _context.TransactionStatusLogs.Add(statusLog);

                    await _context.SaveChangesAsync(cancellationToken);

                    _eventBus.Publish(new MarketPlaceTransactionDisputedEvent(
                            transaction.TenantId,
                            transaction.Id,
                            transaction.MerchantAccountId,
                            transaction.CustomerAccountId.Value,
                            transaction.BrokerAccountId,
                            request.AccountId,
                            request.Complaint
                        ));

                    return result;
                }
            }
        }

        public class CommandResult : BaseCommandResult
        { }

        public class CommandValidator : BaseCommandValidator<Command, CommandResult>
        {
            public CommandValidator()
            {
                RuleFor(x => x.TransactionId).NotEmpty();
                RuleFor(x => x.Complaint).NotEmpty();
            }
        }
    }
}