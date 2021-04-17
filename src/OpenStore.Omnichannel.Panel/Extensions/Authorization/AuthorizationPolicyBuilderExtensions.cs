using System;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable All

namespace OpenStore.Omnichannel.Panel
{
    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder MyRequireRole(this AuthorizationPolicyBuilder authorizationPolicyBuilder, params string[] roles)
        {
            if (roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }

            authorizationPolicyBuilder.Requirements.Add(new MyRolesAuthorizationRequirement(roles));

            return authorizationPolicyBuilder;
        }
    }
}