using MediatR;
using Payscrow.ProjectMilestone.Application.Common.Security;
using Payscrow.ProjectMilestone.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Payscrow.ProjectMilestone.Application.Common.Exceptions;

namespace Payscrow.ProjectMilestone.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IIdentityService _identityService;

        public AuthorizationBehaviour(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>(false);

            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (!_identityService.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException();
                }

                // Role-based authorization
                var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

                if (authorizeAttributesWithRoles.Any())
                {
                    var authorized = false;

                    foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                    {
                        foreach (var role in roles)
                        {
                            var isInRole = _identityService.IsInRole(role.Trim());
                            if (isInRole)
                            {
                                authorized = true;
                                break;
                            }
                        }
                    }

                    // Must be a member of at least one role in roles
                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }

                //// Policy-based authorization
                //var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
                //if (authorizeAttributesWithPolicies.Any())
                //{
                //    foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                //    {
                //        var authorized = await _identityService.AuthorizeAsync(_currentUserService.UserId, policy);

                //        if (!authorized)
                //        {
                //            throw new ForbiddenAccessException();
                //        }
                //    }
                //}
            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}
