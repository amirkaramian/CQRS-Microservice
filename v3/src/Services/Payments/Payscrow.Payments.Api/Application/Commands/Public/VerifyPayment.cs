using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payscrow.Core.Bus;
using Payscrow.Payments.Api.Application.IntegrationEvents;
using Payscrow.Payments.Api.Application.Models;
using Payscrow.Payments.Api.Application.Services.Flutterwave;
using Payscrow.Payments.Api.Data;
using Payscrow.Payments.Api.Domain.Enumerations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Application.Commands.Public
{
    public static class VerifyPayment
    {
        public class Command : BaseCommand<CommandResult>
        {
            public string PaymentMethodId { get; set; }
            public Guid? PaymentId { get; set; }
            public string ExternalTransactionId { get; set; }
            public string Status { get; set; }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly PaymentsDbContext _context;
                private readonly ILogger _logger;
                private readonly FlutterwaveProviderService _flutterwaveProviderService;
                private readonly IEventBus _eventBus;

                public Handler(PaymentsDbContext context, ILogger<Handler> logger, FlutterwaveProviderService flutterwaveProviderService, IEventBus eventBus)
                {
                    _context = context;
                    _logger = logger;
                    _flutterwaveProviderService = flutterwaveProviderService;
                    _eventBus = eventBus;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var paymentMethodId = Guid.Parse(request.PaymentMethodId);
                    var paymentMethod = await _context.PaymentMethods.FindAsync(paymentMethodId);

                    if (paymentMethod == null)
                    {
                        _logger.LogCritical("A request for payment verfication could not find the payment method involved with the following ID: {PaymentMethodId}", request.PaymentMethodId);
                    }

                    var payment = await _context.Payments.Include(x => x.Currency).SingleOrDefaultAsync(x => x.Id == request.PaymentId);

                    if (payment == null)
                    {
                        _logger.LogCritical("A payment set for verification could not be found in the payment logs with ID: {LocalTransactionId}", request.PaymentId);
                    }

                    if (payment.IsPaid)
                    {
                        result.IsVerified = true;
                        return result;
                    }

                    VerificationResponseModel verificationResponse;

                    switch (paymentMethod.Provider)
                    {
                        case PaymentMethodProvider.Flutterwave:

                            verificationResponse = await _flutterwaveProviderService.VerifyTransaction(request.ExternalTransactionId, payment);
                            break;

                        case PaymentMethodProvider.Paystack:
                            verificationResponse = new VerificationResponseModel();
                            break;

                        default:
                            throw new Exception("Payment method type not defined");
                    }

                    if (verificationResponse.IsPaymentVerified)
                    {
                        payment.IsPaid = true;
                        await _context.SaveChangesAsync();

                        _eventBus.Publish(new PaymentVerifiedIntegrationEvent(
                             payment.TransactionGuid.ToString(), payment.Amount, payment.Name, payment.EmailAddress, DateTime.Now, request.TenantId));
                    }

                    result.IsVerified = verificationResponse.IsPaymentVerified;

                    return result;
                }
            }
        }

        public class CommandResult : BaseCommandResult
        {
            public bool IsVerified { get; set; }
        }
    }
}