using FluentValidation;
using MediatR;
using Payscrow.Notifications.Api.Application.Enumerations;
using Payscrow.Notifications.Api.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Application.Commands
{
    public static class SendCreateDealVerificationCode
    {
        public class Command : BaseCommand, IRequest<CommandResult>
        {
            public string SellerEmail { get; set; }
            public string Code { get; set; }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly IEmailNotificationService _emailNotificationService;

                public Handler(IEmailNotificationService emailNotificationService)
                {
                    _emailNotificationService = emailNotificationService;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    await _emailNotificationService.SendAsync(request.TenantId, EmailMessageType.GuestDeal,
                            request.SellerEmail, "Payment Invite Verification Code",
                            new Dictionary<string, object> { { "code", request.Code } });

                    return result;
                }
            }
        }

        public class CommandResult : BaseCommandResult
        {
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.SellerEmail).NotEmpty().EmailAddress();
                RuleFor(x => x.Code).NotEmpty();
            }
        }
    }
}