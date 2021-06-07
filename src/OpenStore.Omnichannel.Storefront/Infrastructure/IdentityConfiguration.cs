using System;
using System.Collections.Generic;

namespace OpenStore.Omnichannel.Storefront.Infrastructure
{
    public class IdentityConfiguration
    {
        public int SessionExpireTimeInMinutes { get; set; } 
        public string Authority { get; init; }
    }
}