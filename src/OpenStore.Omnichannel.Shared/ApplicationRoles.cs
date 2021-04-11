// ReSharper disable once CheckNamespace

namespace OpenStore.Omnichannel
{
    public static class ApplicationRoles
    {
        public const string Customer = nameof(Customer);
        public const string Administrator = nameof(Administrator);
        
        public static string[] AsArray => new[]
        {
            Customer,
            Administrator
        };
    }
}