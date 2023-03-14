using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.Payments.Api.Data;
using Payscrow.Payments.Api.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Application.Commands.Public
{
    public static class CreatePayment
    {
        public class Command : BaseCommand<CommandResult>
        {
            public decimal Amount { get; set; }
            public Guid? TransactionGuid { get; set; }
            public string Name { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string CurrencyCode { get; set; }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly PaymentsDbContext _context;

                public Handler(PaymentsDbContext context)
                {
                    _context = context;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var currency = await _context.Currencies.SingleOrDefaultAsync(x => x.Code == request.CurrencyCode);

                    if (currency == null)
                    {
                        result.Errors.Add(("CurrencyCode", $"{request.CurrencyCode} is not a valid currency code!"));
                        return result;
                    }

                    var payment = new Payment
                    {
                        Amount = request.Amount,
                        TransactionGuid = request.TransactionGuid.Value,
                        Name = request.Name,
                        EmailAddress = request.EmailAddress,
                        PhoneNumber = request.PhoneNumber,
                        CurrencyId = currency.Id,
                        TenantId = request.TenantId
                    };

                    _context.Payments.Add(payment);
                    await _context.SaveChangesAsync();

                    result.PaymentId = payment.Id;

                    return result;
                }
            }
        }

        public class CommandResult : BaseCommandResult
        {
            public Guid PaymentId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
                RuleFor(x => x.TransactionGuid).NotEmpty();
                RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress();
                //RuleFor(x => x.PhoneNumber).NotEmpty();
                RuleFor(x => x.CurrencyCode).NotEmpty().Length(3);
            }
        }
    }
}