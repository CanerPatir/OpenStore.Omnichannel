namespace OpenStore.Omnichannel.Tools.Helpers;

public static class ImageDownloadHelper
{
    public static async Task Download(string url)
    {
        var uri = new Uri(url);
        var filename = Path.GetFileName(uri.PathAndQuery);
        var path = $"content/{filename}";
        if(File.Exists(path))
            return;
        
        using var client = new HttpClient();
        var response = await client.GetAsync(uri);
        await using var fs = new FileStream(path, FileMode.CreateNew);
        await response.Content.CopyToAsync(fs);
    }
}