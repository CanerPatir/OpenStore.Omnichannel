using System;
using System.Collections.Generic;

namespace OpenStore.Omnichannel.Shared.Request
{
    public record UpdateVariantBarcodesRequest(IEnumerable<UpdateVariantBarcodeRequest> Variants);

    public record UpdateVariantBarcodeRequest(Guid VariantId, string Barcode);
}