using System.ComponentModel.DataAnnotations.Schema;

namespace OpenStore.Omnichannel.Domain;

public interface IMediaEntity
{
    string Host { get; }
    string Path { get; }
    string Type { get; }
    string Extension { get; }
    string Filename { get; }
    string Title { get; }
    int Position { get; }
    long? Size { get; }
}