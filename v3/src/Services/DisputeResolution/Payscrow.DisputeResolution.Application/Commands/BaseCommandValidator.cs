using FluentValidation;

namespace Payscrow.DisputeResolution.Application.Commands
{
    public class BaseCommandValidator<C,CR> : AbstractValidator<C> where C : BaseCommand<CR> where CR : BaseCommandResult
    {
        public BaseCommandValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
        }
    }
}
