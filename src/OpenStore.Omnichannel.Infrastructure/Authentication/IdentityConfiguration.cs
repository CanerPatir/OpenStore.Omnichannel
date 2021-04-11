
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OpenStore.Omnichannel
{
    public class IdentityConfiguration
    {
        public int SessionExpireTimeInMinutes { get; set; } 
        public string Authority { get; init; }
        public HashSet<Uri> WebPostLogoutRedirectUris { get; init; }
        public HashSet<Uri> WebRedirectUris { get; init; }
        public HashSet<Uri> PanelPostLogoutRedirectUris { get; init; }
        public HashSet<Uri> PanelRedirectUris { get; init; }
    }
}