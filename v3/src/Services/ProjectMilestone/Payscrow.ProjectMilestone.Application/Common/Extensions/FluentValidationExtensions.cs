using FluentValidation;
using System;

namespace Payscrow.ProjectMilestone.Application.Common.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> IsValidGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => Guid.TryParse(x, out Guid _)).WithMessage("parameter is invalid");
        }
    }
}
