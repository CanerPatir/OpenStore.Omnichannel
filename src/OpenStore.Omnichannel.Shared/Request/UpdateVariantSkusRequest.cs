using System;
using System.Collections.Generic;

namespace OpenStore.Omnichannel.Shared.Request
{
    public record UpdateVariantSkusRequest(IEnumerable<UpdateVariantSkuRequest> Variants);

    public record UpdateVariantSkuRequest(Guid VariantId, string Sku);
}