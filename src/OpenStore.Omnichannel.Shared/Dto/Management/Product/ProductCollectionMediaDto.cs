using System.ComponentModel.DataAnnotations.Schema;

namespace OpenStore.Omnichannel.Shared.Dto.Management.Product;

public record ProductCollectionMediaDto(string Host, string Path, string Type, string Extension, string Filename, string Title, int Position, long? Size)
{
    [NotMapped] public string Url => GeneralHelper.GetMediaUrl(Host, Path);
}