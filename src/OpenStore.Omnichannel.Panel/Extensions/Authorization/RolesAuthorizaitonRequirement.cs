using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable All

namespace OnlineCourse.Panel
{
    public class MyRolesAuthorizationRequirement : AuthorizationHandler<MyRolesAuthorizationRequirement>, IAuthorizationRequirement
    {
        public MyRolesAuthorizationRequirement(IEnumerable<string> allowedRoles)
        {
            if (allowedRoles is null)
            {
                throw new ArgumentNullException(nameof(allowedRoles));
            }

            if (!allowedRoles.Any())
            {
                throw new InvalidOperationException(nameof(allowedRoles));
            }

            AllowedRoles = allowedRoles;
        }

        public IEnumerable<string> AllowedRoles { get; }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MyRolesAuthorizationRequirement requirement)
        {
            if (context.User != null)
            {
                bool found = false;
                if (requirement.AllowedRoles == null || !requirement.AllowedRoles.Any())
                {
                    // Review: What do we want to do here?  No roles requested is auto success?
                }
                else
                {
                    found = requirement.AllowedRoles.Any(r => IsInRole(context.User, r));
                }

                if (found)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }

        private bool IsInRole(ClaimsPrincipal user, string role) => user.InRole(role);

        /// <inheritdoc />
        public override string ToString()
        {
            var roles = $"User.IsInRole must be true for one of the following roles: ({string.Join("|", AllowedRoles)})";

            return $"{nameof(MyRolesAuthorizationRequirement)}:{roles}";
        }
    }
}