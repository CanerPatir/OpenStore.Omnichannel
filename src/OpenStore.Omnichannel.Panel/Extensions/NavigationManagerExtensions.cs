// ReSharper disable All

using System;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Components;

public static class NavigationManagerExtensions
{
    public static void ToSiteHome(this NavigationManager navManager)
    {
        navManager.NavigateTo("/");
    }

    public static void Reload(this NavigationManager navManager)
    {
        navManager.NavigateTo(navManager.Uri, forceLoad: true);
    }
}