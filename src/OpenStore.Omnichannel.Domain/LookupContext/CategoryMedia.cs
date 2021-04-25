using System;

namespace OpenStore.Omnichannel.Domain.LookupContext
{
    public class CategoryMedia : MediaEntity
    {
        public Guid? CategoryId { get; set; }
        public virtual Category Category { get; protected set; }
        
        public CategoryMedia(string host, string path, string type, string extension, string filename) : base(host, path, type, extension, filename)
        {
        }
    }
}