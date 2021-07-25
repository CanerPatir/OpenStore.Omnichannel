// ReSharper disable once CheckNamespace

namespace OpenStore.Omnichannel
{
    public static class ApplicationRoles
    {
        public const string Customer = nameof(Customer);
        public const string Administrator = nameof(Administrator);
        public const string StoreOwner = nameof(StoreOwner);
        public const string StoreAdmin = nameof(StoreAdmin);

        public static string[] AsArray => new[]
        {
            Customer,
            Administrator,
            StoreOwner,
            StoreAdmin
        };
    }
}