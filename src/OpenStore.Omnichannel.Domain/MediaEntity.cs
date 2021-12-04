// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBeProtected.Global

using System.ComponentModel.DataAnnotations.Schema;

namespace OpenStore.Omnichannel.Domain;

public abstract class MediaEntity : AuditableEntity, IMediaEntity
{
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