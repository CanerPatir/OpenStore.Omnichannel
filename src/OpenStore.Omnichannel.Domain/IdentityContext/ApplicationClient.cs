using System;
using OpenIddict.EntityFrameworkCore.Models;

// ReSharper disable ClassNeverInstantiated.Global

namespace OpenStore.Omnichannel.Domain.IdentityContext
{
    public class ApplicationClient : OpenIddictEntityFrameworkCoreApplication<Guid, ApplicationAuthorization, ApplicationToken>
    {
    }

    public class ApplicationAuthorization : OpenIddictEntityFrameworkCoreAuthorization<Guid, ApplicationClient, ApplicationToken>
    {
    }

    public class ApplicationScope : OpenIddictEntityFrameworkCoreScope<Guid>
    {
    }

    public class ApplicationToken : OpenIddictEntityFrameworkCoreToken<Guid, ApplicationClient, ApplicationAuthorization>
    {
    }
}