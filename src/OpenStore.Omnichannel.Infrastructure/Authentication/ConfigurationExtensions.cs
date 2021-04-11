using Microsoft.Extensions.Configuration;

// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel
{
    public static class ConfigurationExtensions
    {
        public static IdentityConfiguration GetIdentityConfiguration(this IConfiguration configuration)
        {
            return configuration.GetSection("IdentityConfiguration").Get<IdentityConfiguration>();
        }
    }
}