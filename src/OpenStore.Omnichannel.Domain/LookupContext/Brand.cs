using System.Collections.Generic;

namespace OpenStore.Omnichannel.Domain.LookupContext;

public class Brand : LookupEntity
{
    public virtual List<BrandMedia> Medias { get; set; } = new();
}