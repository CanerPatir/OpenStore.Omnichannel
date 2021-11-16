namespace OpenStore.Omnichannel;

public static class GeneralHelper
{
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
}