namespace OpenStore.Omnichannel.Domain.LookupContext;

public class BrandMedia : MediaEntity
{
    public Guid? BrandId { get; set; }
    public virtual Brand Brand { get; protected set; }

    protected BrandMedia()
    {
    }
}