using HPC.Application.Common.Exceptions;
using HPC.Application.Common.Interfaces;
using HPC.Application.Common.Security;
using MediatR;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace HPC.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public AuthorizationBehaviour(ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
            if (authorizeAttributes.Any())
            {
                if (_currentUserService.UserId is null)
                    throw new UnauthorizedAccessException();

                await VerifyRoleBasedAuthorization(authorizeAttributes);

                await VerifyPolicyBasedAuthorization(authorizeAttributes);
            }

            return await next();
        }

        private async Task VerifyRoleBasedAuthorization(System.Collections.Generic.IEnumerable<AuthorizeAttribute> authorizeAttributes)
        {
            var authorizeAttributesWithRoles = authorizeAttributes.Where(auth => string.IsNullOrWhiteSpace(auth.Roles) is false);
            if (authorizeAttributesWithRoles.Any())
            {
                foreach (var roles in authorizeAttributesWithRoles.Select(auth => auth.Roles.Split(',')))
                {
                    var authorized = false;
                    foreach (var role in roles)
                    {
                        var isInRole = await _identityService.IsInRoleAsync(_currentUserService.UserId, role.Trim());
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }

                    if (authorized is false)
                        throw new ForbiddenAccessException();
                }
            }
        }

        private async Task VerifyPolicyBasedAuthorization(System.Collections.Generic.IEnumerable<AuthorizeAttribute> authorizeAttributes)
        {
            var authorizeAttributesWithPolicies = authorizeAttributes.Where(auth => !string.IsNullOrWhiteSpace(auth.Policy));
            if (authorizeAttributesWithPolicies.Any())
            {
                foreach (var policy in authorizeAttributesWithPolicies.Select(auth => auth.Policy))
                {
                    var authorized = await _identityService.AuthorizeAsync(_currentUserService.UserId, policy);
                    if (authorized is false)
                        throw new ForbiddenAccessException();
                }
            }
        }
    }
}
