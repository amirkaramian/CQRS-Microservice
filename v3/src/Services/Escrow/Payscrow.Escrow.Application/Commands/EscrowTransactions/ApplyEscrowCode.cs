using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payscrow.Core.Bus;
using Payscrow.Escrow.Application.IntegrationEvents.Publishing;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Domain.Enumerations;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Payscrow.Escrow.Application.Commands.EscrowTransactions
{
    public static class ApplyEscrowCode
    {
        public class Command : BaseCommand<CommandResult>
        {
            public Guid TransactionGuid { get; set; }
            public string Code { get; set; }
            public Guid AccountId { get; set; }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly IEscrowDbContext _context;
                private readonly IEventBus _eventBus;
                private readonly ILogger _logger;

                public Handler(IEscrowDbContext context, IEventBus eventBus, ILogger<Handler> logger)
                {
                    _context = context;
                    _eventBus = eventBus;
                    _logger = logger;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var transaction = await (from et in _context.EscrowTransactions.ForTenant(request.TenantId)
                                             join a in _context.Accounts on et.OwnerAccountId equals a.Id
                                             where a.AccountGuid == request.AccountId && et.TransactionGuid == request.TransactionGuid
                                             select et)
                                            .SingleOrDefaultAsync();

                    if (transaction is null)
                    {
                        _logger.LogWarning("---- Transaction could not be found while trying to apply escrow code. Transaction ID: {TransactionID} ----", request.TransactionGuid);
                        result.Errors.Add(("TransactionId", "Transaction could not be found!"));
                        return result;
                    }

                    if (!transaction.ServiceType.CanApplyEscrowCode)
                    {
                        result.Errors.Add(("ServiceType", "Escrow code cannot be applied on this transaction!"));
                        return result;
                    }

                    if (transaction.InDispute)
                    {
                        result.Errors.Add(("Disputed", "you cannot apply an escrow code to a transaction that is in Dispute!"));
                        return result;
                    }

                    if (!string.Equals(transaction.EscrowCode, request.Code, StringComparison.OrdinalIgnoreCase))
                    {
                        result.Errors.Add(("Code", "invalid code. pls ensure you have inputed the correct code!"));
                        return result;
                    }

                    transaction.StatusId = EscrowTransactionStatus.PendingSettlement.Id;
                    transaction.IsReleased = true;

                    await _context.SaveChangesAsync(cancellationToken);

                    _eventBus.Publish(new EscrowCodeAppliedEvent(request.TenantId, transaction.TransactionGuid));

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
                RuleFor(x => x.Code).NotEmpty();
                RuleFor(x => x.TransactionGuid).NotEmpty();
                RuleFor(x => x.AccountId).NotEmpty();
            }
        }
    }
}