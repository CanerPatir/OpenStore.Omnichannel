using System;

namespace OpenStore.Omnichannel.Domain.LookupContext;

public class CategoryMedia : MediaEntity
{
    public Guid? CategoryId { get; set; }
    public virtual Category Category { get; protected set; }

    protected CategoryMedia()
    {
    }
}