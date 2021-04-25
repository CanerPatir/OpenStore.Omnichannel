// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace OpenStore.Omnichannel.Domain
{
    public abstract class MediaEntity : AuditableEntity
    {
        public string Host { get; protected set; }
        public string Path { get; protected set; }
        public string Type { get; protected set; }
        public string Extension { get; protected set; }
        public string Filename { get; protected set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public long? Size { get; set; }

        protected MediaEntity(string host, string path, string type, string extension, string filename)
        {
            Host = host;
            Path = path;
            Type = type;
            Extension = extension;
            Filename = filename;
        }
    }
}