using FluentValidation;

namespace Payscrow.Notifications.Api.Application.Commands
{
    public class BaseCommandValidator<T> : AbstractValidator<T> where T : BaseCommand
    {
        public BaseCommandValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
        }
    }
}
