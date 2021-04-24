using System;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.LookupContext
{
    public abstract class LookupEntity : Entity<Guid>
    {
        public string Title { get; set; }
        public string DisplayTitle { get; set; }
        public string Description { get; set; }
    }
}