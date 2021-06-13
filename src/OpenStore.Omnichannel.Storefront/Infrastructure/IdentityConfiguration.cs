using System;
using System.Collections.Generic;

namespace OpenStore.Omnichannel.Storefront.Infrastructure
{
    public class IdentityConfiguration
    {
        public string Authority { get; init; }
        public int SessionExpireTimeInMinutes { get; init; } 
    }
}