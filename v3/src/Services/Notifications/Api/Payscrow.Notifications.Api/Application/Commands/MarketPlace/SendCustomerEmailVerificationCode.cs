using FluentValidation;
using MediatR;
using Payscrow.Notifications.Api.Application.Enumerations;
using Payscrow.Notifications.Api.Application.Interfaces;
using Payscrow.Notifications.Api.Application.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Application.Commands.MarketPlace
{
    public static class SendCustomerEmailVerificationCode
    {
        public class Command : BaseCommand, IRequest<CommandResult>
        {
            public string EmailAddress { get; set; }
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

                    //var emailContent = await _emailNotificationService.GetEmailContent(EmailMessageType.EmailVerificationCode,
                    //        new Dictionary<string, object> { { "code", request.Code } }
                    //    );

                    //var emailLog = new EmailLog {
                    //    TenantId = request.TenantId,
                    //    Content = emailContent
                    //};

                    await _emailNotificationService.SendAsync(
                        request.TenantId,
                        EmailMessageType.EmailVerificationCode,
                        request.EmailAddress,
                        "Email Verification Code",
                        new Dictionary<string, object> { { "code", request.Code } });

                    return result;
                }
            }
        }

        public class CommandResult : BaseCommandResult
        {
        }

        public class CommandValidator : BaseCommandValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress().MaximumLength(200);
                RuleFor(x => x.Code).NotEmpty().MaximumLength(30);
            }
        }
    }
}