using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.Core.Bus;
using Payscrow.DisputeResolution.Application.Domain.Entities;
//using Payscrow.DisputeResolution.Application.Domain.Enumerations;
//using Payscrow.DisputeResolution.Application.IntegrationEvents.Publishing;
using Payscrow.DisputeResolution.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.DisputeResolution.Application.Commands.Business
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
                private readonly IDisputeDbContext _context;
                private readonly IEventBus _eventBus;

                public Handler(IDisputeDbContext context, IEventBus eventBus)
                {
                    _context = context;
                    _eventBus = eventBus;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    //Check if transaction already exist in dispute table
                    var checkDisputedTransaction = await _context.DisputedTransactions.FirstOrDefaultAsync(x => x.TransactionId == request.TransactionId && x.AccountId == request.AccountId);

                    if (checkDisputedTransaction != null)
                    {
                        result.Errors.Add(("Disputed Transaction", "This transaction already disputed, Check dashboard for status!"));
                        return result;
                    }

                    //Create disputed transaction payload
                    var logDisputedTransaction = new DisputedTransaction
                    {
                        TransactionId = request.TransactionId,
                        AccountId = request.AccountId,
                        StatusId = 1,
                        InDispute = true,
                        DisputeDescription = request.Complaint,
                        DisputeArbitrationLevelId = 1,
                        CreateUtc = DateTime.UtcNow

                    };

                    //Create disputed transaction payload for initial dispute chat history
                    var logDisputedTransactionChat = new DisputedTransactionChatRecord
                    {
                        TransactionId = request.TransactionId,
                        AccountId = request.AccountId,
                        DisputeChats = request.Complaint,                         
                        CreateUtc = DateTime.UtcNow

                    };
                    _context.DisputedTransactions.Add(logDisputedTransaction);
                    _context.DisputedTransactionChatRecords.Add(logDisputedTransactionChat);

                    await _context.SaveChangesAsync(cancellationToken);

                    //_eventBus.Publish(new DisputedTransactionsDisputedEvent(
                    //        transaction.TenantId,
                    //        transaction.Id,
                    //        transaction.MerchantAccountId,
                    //        transaction.CustomerAccountId.Value,
                    //        transaction.BrokerAccountId,
                    //        request.AccountId,
                    //        request.Complaint
                    //    ));

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