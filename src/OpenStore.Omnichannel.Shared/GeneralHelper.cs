// ReSharper disable CheckNamespace

using System.Globalization;

namespace OpenStore.Omnichannel;

public static class GeneralHelper
{
    private static readonly SlugHelper SlugHelper = new();

    public static string GetVariantTitle(string option1, string option2, string option3)
    {
        var title = option1;
        if (!string.IsNullOrWhiteSpace(option2))
        {
            title += $" / {option2}";
        }

        if (!string.IsNullOrWhiteSpace(option3))
        {
            title += $" / {option3}";
        }

        return title;
    }

    public static string GetMediaUrl(string host, string path) => $"{host?.TrimEnd('/')}/{path}";

    public static string GenerateSlug(string str) => SlugHelper.GenerateSlug(str);

    public static string GenerateSlug(string str, CultureInfo cultureInfo) => SlugHelper.GenerateSlug(str, cultureInfo);
}