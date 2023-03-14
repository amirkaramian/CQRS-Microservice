using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.ProjectMilestone.Application.Common.Extensions;
using Payscrow.ProjectMilestone.Application.Common.Security;
using Payscrow.ProjectMilestone.Application.Common.Utilities;
using Payscrow.ProjectMilestone.Application.Domain.Enumerations;
using Payscrow.ProjectMilestone.Application.Domain.Models;
using Payscrow.ProjectMilestone.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.ProjectMilestone.Application.Commands.Projects
{
    public static class CreateProject
    {
        [Authorize]
        public class Command : BaseCommand<CommandResult>
        {
            public string Title { get; set; }
            public string CurrencyId { get; set; }
            public string Role { get; set; }
            public decimal? TotalAmount { get; set; }
            public string PartnerEmailAddress { get; set; }
            public string InviteResponseUrl { get; set; }

            private class Handler : IRequestHandler<Command, CommandResult>
            {
                private readonly IMilestoneDbContext _context;
                private readonly IIdentityService _identityService;
                private readonly IInviteNotificationService _inviteNotificationService;

                public Handler(IIdentityService identityService, IMilestoneDbContext context, IInviteNotificationService inviteNotificationService)
                {
                    _identityService = identityService;
                    _context = context;
                    _inviteNotificationService = inviteNotificationService;
                }

                public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
                {
                    var result = new CommandResult();

                    var accounId = _identityService.AccountId;
                    var userId = _identityService.UserIdentity;
                    var tenantId = _identityService.TenantId;

                    if (!accounId.HasValue || !userId.HasValue || !tenantId.HasValue)
                    {
                        result.Errors.Add(("", "user not authorized!"));
                        return result;
                    }

                    var currencyId = request.CurrencyId.ToGuid();

                    if (!await _context.Currencies.AnyAsync(x => x.Id == currencyId))
                    {
                        result.Errors.Add(("CurrencyId", "Invalid currency Id!"));
                        return result;
                    }

                    var project = new Project
                    {
                        Title = request.Title,
                        TotalAmount = request.TotalAmount,
                        CurrencyId = request.CurrencyId.ToGuid(),
                        CreateUserId = userId.Value,
                        AccountId = accounId.Value,
                        TenantId = tenantId.Value
                    };

                    var invite = new Invite
                    {
                        PartnerEmailAddress = request.PartnerEmailAddress,
                        ProjectId = project.Id,
                        CreateUserId = userId.Value,
                        AccountId = accounId.Value,
                        TenantId = tenantId.Value
                    };

                    switch (EnumUtility.GetEnumValueOrDefault<ProjectPartnerRoleType>(request.Role))
                    {
                        case ProjectPartnerRoleType.Owner:
                            project.PayerAccountName = _identityService.AccountName;
                            project.PayerAccountId = _identityService.AccountId;
                            invite.RoleType = ProjectPartnerRoleType.Implementer;
                            break;

                        case ProjectPartnerRoleType.Implementer:
                            project.PayeeAccountName = _identityService.AccountName;
                            project.PayeeAccountId = _identityService.AccountId;
                            invite.RoleType = ProjectPartnerRoleType.Owner;
                            break;
                    }

                    invite.ResponseLink = $"{request.InviteResponseUrl}?inviteId={invite.Id}";

                    _context.Projects.Add(project);
                    _context.Invites.Add(invite);

                    await _context.SaveChangesAsync(cancellationToken);

                    result.ProjectId = project.Id;

                    await _inviteNotificationService.SendAsync(
                        new InviteNotificationRequest
                        {
                            AccountName = _identityService.AccountName,
                            InviteId = invite.Id,
                            TenantId = _identityService.TenantId.Value
                        });

                    return result;
                }
            }
        }

        public class CommandResult : BaseCommandResult
        {
            public Guid? ProjectId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
                RuleFor(x => x.PartnerEmailAddress).NotEmpty().EmailAddress().MaximumLength(200);
                RuleFor(x => x.CurrencyId).NotEmpty().IsValidGuid().WithMessage(x => $"'{x.CurrencyId}' is invalid!");
                RuleFor(x => x.Role).Must(x => EnumUtility.TryParseWithMemberName<ProjectPartnerRoleType>(x, out _))
                    .WithMessage(x => $"{x.Role} is not a valid value for Role").NotEmpty();
                RuleFor(x => x.InviteResponseUrl).NotEmpty();
            }
        }
    }
}