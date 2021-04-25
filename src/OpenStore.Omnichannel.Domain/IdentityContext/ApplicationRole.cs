using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.IdentityContext
{
    public class ApplicationRole : IdentityRole<Guid>, IEntity
    {
        [NotMapped] object IEntity.Id => Id;
        public long Version { get; set; }
    }
}