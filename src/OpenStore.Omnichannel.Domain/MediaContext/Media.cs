using System;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.MediaContext
{
    public class Media : Entity<Guid>
    {
        public Guid? ProductId { get; set; }
        public string Host { get; protected set; }
        public string Path { get; protected set; }
        public string Type { get; protected set; }
        public string Extension { get; protected set; }
        public string Filename { get; protected set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public long? Size { get; set; }

        public Media(string host, string path, string type, string extension, string filename)
        {
            Host = host;
            Path = path;
            Type = type;
            Extension = extension;
            Filename = filename;
        }
    }
}