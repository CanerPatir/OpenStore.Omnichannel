using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain;

public abstract class AuditableEntity : Entity<Guid>, IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
}