using System;
using System.ComponentModel.DataAnnotations;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.ChannelContext
{
    public class SaleChannel : Entity<Guid>
    {
        [Required] public string Name { get; set; }
        public string Description { get; set; }
    }
}