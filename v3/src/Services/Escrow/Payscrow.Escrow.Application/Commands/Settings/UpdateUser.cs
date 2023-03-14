using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.Commands.Settings
{
    public static class UpdateUser
    {
        public class Command : BaseCommand<CommandResult>
        {
            public Guid UserGuid { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string AvatarFileName { get; set; }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly IEscrowDbContext _context;
                private readonly ILogger _logger;
                private readonly UserService _userService;

                public Handler(IEscrowDbContext context, ILogger<Handler> logger, UserService userService)
                {
                    _context = context;
                    _logger = logger;
                    _userService = userService;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var user = await _context.Users.SingleOrDefaultAsync(x => x.UserGuid == request.UserGuid && x.TenantId == request.TenantId);

                    if (user == null)
                    {
                        _logger.LogWarning("No user was found to be updated");
                        result.Errors.Add(("UserGuid", "Invalid user"));
                        return result;
                    }

                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.MiddleName = request.MiddleName;
                    user.PhoneNumber = request.PhoneNumber;
                    user.Email = request.Email;

                    _userService.RemoveUserFromCache(request.UserGuid, request.TenantId);

                    await _context.SaveChangesAsync();

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
                RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
                RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
                RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
                RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);
            }
        }
    }
}