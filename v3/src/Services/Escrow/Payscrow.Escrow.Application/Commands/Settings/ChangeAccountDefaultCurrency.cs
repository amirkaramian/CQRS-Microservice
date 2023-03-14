using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Application.Services;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using FluentValidation;

namespace Payscrow.Escrow.Application.Commands.Settings
{
    public static class ChangeAccountDefaultCurrency
    {
        public class Command : BaseCommand<CommandResult>
        {
            public string CurrencyCode { get; set; }
            public Guid AccountId { get; set; }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly IEscrowDbContext _context;
                private readonly AccountSettingService _accountSettingService;

                public Handler(IEscrowDbContext context, AccountSettingService accountSettingService)
                {
                    _context = context;
                    _accountSettingService = accountSettingService;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var accountSetting = await _accountSettingService.GetAccountSettingAsync(request.AccountId, request.TenantId, true);

                    if (accountSetting is null)
                    {
                        result.Errors.Add(("AccountId", "Invalid account ID!"));
                        return result;
                    }

                    var currencyId = await _context.Currencies
                        .Where(x => x.Code == request.CurrencyCode && x.TenantId == request.TenantId)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();

                    if (currencyId == Guid.Empty)
                    {
                        result.Errors.Add(("CurrencyCode", "Invalid currency code!"));
                        return result;
                    }

                    accountSetting.DefaultCurrencyId = currencyId;

                    await _context.SaveChangesAsync();

                    _accountSettingService.RemoveAccountSettingFromCache(request.AccountId);

                    return result;
                }
            }
        }

        public class CommandResult : BaseCommandResult
        {
        }

        public class CommandValidator : BaseCommandValidator<Command, CommandResult>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AccountId).NotEmpty();
            }
        }
    }
}