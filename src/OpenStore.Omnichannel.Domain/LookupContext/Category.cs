namespace OpenStore.Omnichannel.Domain.LookupContext;

public class Category : LookupEntity
{
    public Guid? ParentId { get; set; }
    public virtual Category Parent { get; set; }
    public virtual ICollection<Category> Children { get; set; } = new List<Category>();

    public virtual List<CategoryMedia> Medias { get; set; } = new();
}