using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationException = Payscrow.Escrow.Application.Common.Exceptions.ValidationException;

namespace Payscrow.Escrow.Application.Common.Behaviours
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            var response = await next();

            //if (response.Errors.Count != 0)
            //{
            //    var domainFailures = response.Errors.ConvertAll(x => new ValidationFailure(x.Item1, x.Item2));
            //    throw new ValidationException(domainFailures);
            //}

            return response;
        }
    }
}