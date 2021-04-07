using System;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.LookupContext
{
    public class Tag : Entity<Guid>
    {
        public string Name { get; set; }
    }
}