// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

using System.ComponentModel.DataAnnotations.Schema;

namespace OpenStore.Omnichannel.Domain.ProductContext;

public class ProductCollectionMedia : IMediaEntity
{
    protected ProductCollectionMedia()
    {
    }

    public static ProductCollectionMedia Create(string host, string path, string type, string extension, string filename, string title, int position, long? size)
    {
        return new ProductCollectionMedia()
        {
            Host = host,
            Path = path,
            Type = type,
            Extension = extension,
            Filename = filename,
            Title = title,
            Position = position,
            Size = size
        };
    }

    public string Host { get; protected set; }
    public string Path { get; protected set; }
    public string Type { get; protected set; }
    public string Extension { get; protected set; }
    public string Filename { get; protected set; }
    public string Title { get; protected set; }
    public int Position { get; protected set; }
    public long? Size { get; protected set; }
    [NotMapped] public string Url => GeneralHelper.GetMediaUrl(Host, Path);
}