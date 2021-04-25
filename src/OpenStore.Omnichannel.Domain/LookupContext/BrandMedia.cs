using System;

namespace OpenStore.Omnichannel.Domain.LookupContext
{
    public class BrandMedia : MediaEntity
    {
        public Guid? BrandId { get; set; }
        public virtual Brand Brand { get; protected set; }
        
        public BrandMedia(string host, string path, string type, string extension, string filename) : base(host, path, type, extension, filename)
        {
        }
    }
}