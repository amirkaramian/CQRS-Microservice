using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.Payments.Api.Application.Models;
using Payscrow.Payments.Api.Application.Services.Flutterwave;
using Payscrow.Payments.Api.Data;
using Payscrow.Payments.Api.Domain.Enumerations;
using Payscrow.Payments.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Application.Commands.Business
{
    public static class SendSettlementPayment
    {
        public class Command : BaseCommand<CommandResult>
        {
            public Guid TransactionGuid { get; set; }
            public List<SettlementDto> Settlements { get; set; }

            public class SettlementDto
            {
                public string BankCode { get; set; }
                public string AccountNumber { get; set; }
                public string AccountName { get; set; }
                public decimal Amount { get; set; }
            }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly PaymentsDbContext _context;
                private readonly FlutterwaveSettlementService _flutterwaveSettlementService;

                public Handler(PaymentsDbContext context, FlutterwaveSettlementService flutterwaveSettlementService)
                {
                    _context = context;
                    _flutterwaveSettlementService = flutterwaveSettlementService;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    if (await _context.Settlements.AnyAsync(x => x.TransactionGuid == request.TransactionGuid))
                    {
                        result.Errors.Add(("TransactionGuid", "This settlement already initiated"));
                        return result;
                    }

                    var initialTransactionPayment = await _context.Payments.Include(x => x.PaymentMethod).Include(x => x.Currency)
                        .OrderByDescending(x => x.CreateUtc)
                        .FirstOrDefaultAsync(x => x.TransactionGuid == request.TransactionGuid && x.TenantId == request.TenantId);

                    if (initialTransactionPayment is null)
                    {
                        result.Errors.Add(("TransactionGuid", "Initial transaction was not found hence settlement cannot be made."));
                        return result;
                    }

                    var settlement = new Settlement
                    {
                        TransactionGuid = request.TransactionGuid,
                        Status = SettlementStatus.Pending,
                        Provider = initialTransactionPayment.PaymentMethod.Provider,
                        TenantId = request.TenantId,
                        CurrencyCode = initialTransactionPayment.Currency.Code
                    };

                    _context.Settlements.Add(settlement);

                    var settlementAccounts = new List<SettlementAccount>();

                    foreach (var settlementItem in request.Settlements)
                    {
                        var settlementAccount = new SettlementAccount
                        {
                            AccountName = "",
                            AccountNumber = settlementItem.AccountNumber,
                            Amount = settlementItem.Amount,
                            BankCode = settlementItem.BankCode,
                            TenantId = request.TenantId,
                            SettlementId = settlement.Id,
                            Status = SettlementStatus.Pending
                        };
                        settlementAccounts.Add(settlementAccount);
                    }

                    _context.SettlementAccounts.AddRange(settlementAccounts);

                    SettlementInitiationResponseModel initiateResponse = new SettlementInitiationResponseModel { IsInitiated = false };

                    switch (initialTransactionPayment.PaymentMethod.Provider)
                    {
                        case PaymentMethodProvider.Flutterwave:
                            initiateResponse = await _flutterwaveSettlementService
                                .InitiateBulkTransfer(settlementAccounts, initialTransactionPayment.Currency.Code, "Escrow payment settlement");

                            settlement.GatewayReference = initiateResponse.GatewayReference;
                            if (initiateResponse.IsInitiated)
                            {
                                settlement.Status = SettlementStatus.Initiated;
                                foreach (var settlementAccount in settlementAccounts)
                                {
                                    settlementAccount.Status = SettlementStatus.Initiated;
                                }
                            }
                            break;

                        case PaymentMethodProvider.Paystack:
                            break;
                    }

                    if (!initiateResponse.IsInitiated)
                    {
                        result.Errors.Add(("", "settlement payment could not be initiated"));
                    }

                    await _context.SaveChangesAsync(cancellationToken);

                    return result;
                }
            }
        }

        public class CommandResult : BaseCommandResult
        {
        }
    }
}