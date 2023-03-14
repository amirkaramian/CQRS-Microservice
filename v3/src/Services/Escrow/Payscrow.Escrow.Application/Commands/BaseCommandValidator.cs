using FluentValidation;

namespace Payscrow.Escrow.Application.Commands
{
    public class BaseCommandValidator<T, TR> : AbstractValidator<T> where T : BaseCommand<TR> where TR : BaseCommandResult
    {
        public BaseCommandValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
        }
    }
}