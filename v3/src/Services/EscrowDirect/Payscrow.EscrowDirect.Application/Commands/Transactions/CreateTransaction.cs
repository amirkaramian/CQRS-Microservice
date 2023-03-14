using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Payscrow.Core.Interfaces;
using Payscrow.EscrowDirect.Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.EscrowDirect.Application.Commands.Transactions
{
    public static class CreateTransaction
    {
        public class Command : IRequest<CommandResult>
        {

            public List<Item> Items { get; set; }

            public class Item
            {
                public string Name { get; set; }
                public string Description { get; set; }
                public decimal Price { get; set; }
                public int Quantity { get; set; }
            }


            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly ITenantService _tenantService;
                private readonly ILogger _logger;

                public Handler(ITenantService tenantService, ILogger<Handler> logger)
                {
                    _tenantService = tenantService;
                    _logger = logger;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var tenantId = await _tenantService.GetTenantIdAsync();

                    if(tenantId == Guid.Empty)
                    {
                        result.Errors.Add(("", "invalid request"));
                        return result;
                    }

                    var transaction = new Transaction {
                        TenantId = tenantId,
                        
                    };


                    return result;
                }
            }
        }

        public class CommandResult : BaseCommandResult { }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Items).Must(x => x.Count > 0).WithMessage("must have at least one item to pay for.");

                RuleForEach(x => x.Items).SetValidator(new ItemValidator());
            }

            public class ItemValidator : AbstractValidator<Command.Item>
            {
                public ItemValidator()
                {
                    RuleFor(x => x.Name).NotEmpty().MaximumLength(300);
                    RuleFor(x => x.Description).MaximumLength(2000);
                    RuleFor(x => x.Price).GreaterThan(0).NotEmpty();
                    RuleFor(x => x.Quantity).GreaterThan(0).NotEmpty();
                }
            }
        }
    }
}
