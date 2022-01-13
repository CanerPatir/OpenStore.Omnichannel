using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.IdentityContext;

public class ApplicationUser : IdentityUser<Guid>, IEntity, IAuditableEntity
{
    [NotMapped] object IEntity.Id => Id;
    public long Version { get; set; }

    [PersonalData] public DateTime? BirthDate { get; set; }
    [Required] [PersonalData] public string Name { get; set; }
    [Required] [PersonalData] public string Surname { get; set; }

    [PersonalData] public string Tckn { get; set; }
    [PersonalData] public string PhotoPath { get; set; }
    [PersonalData] public GenderEnum? Gender { get; set; }

    [Timestamp] public byte[] RowVersion { get; set; }

    [NotMapped] public string Fullname => $"{Name} {Surname}";

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }

    public virtual List<ApplicationUserAddress> Addresses { get; set; } = new();
}