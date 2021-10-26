// ReSharper disable CheckNamespace

using System.Globalization;
using Microsoft.JSInterop;

namespace OpenStore.Omnichannel.Panel;

public static class LocalizationExtensions
{
    private static readonly CultureInfo DefaultCultureInfo = new CultureInfo("tr-TR");

    public static void InitCulture(this IJSInProcessRuntime jsRuntime)
    {
        var result = jsRuntime.Invoke<string>("appCulture.get");
        CultureInfo culture;
        if (result == null)
        {
            culture = DefaultCultureInfo;
        }
        else
        {
            culture = new CultureInfo(result);
        }

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }

    public static void SetCulture(this IJSInProcessRuntime jsRuntime, CultureInfo culture)
    {
        jsRuntime.InvokeVoid("appCulture.set", culture.Name);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }

    public static CultureInfo GetCulture(this IJSInProcessRuntime jsRuntime)
    {
        var cultureName = jsRuntime.Invoke<string>("appCulture.get");
        return cultureName != null ? new CultureInfo(cultureName) : DefaultCultureInfo;
    }
}