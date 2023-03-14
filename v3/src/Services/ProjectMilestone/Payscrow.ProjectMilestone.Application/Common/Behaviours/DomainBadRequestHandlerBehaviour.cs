﻿//using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Payscrow.ProjectMilestone.Application.Commands;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = Payscrow.ProjectMilestone.Application.Common.Exceptions.ValidationException;

namespace Payscrow.ProjectMilestone.Application.Common.Behaviours
{
    public class DomainBadRequestHandlerBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            if (response is BaseCommandResult)
            {
                var r = response as BaseCommandResult;

                if (!r.Success)
                {
                    var failures = r.Errors.Select(x => new ValidationFailure(x.Item1, x.Item2)).ToList();
                    throw new ValidationException(failures);
                }
            }
            return response;
        }
    }
}
