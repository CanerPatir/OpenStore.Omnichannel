namespace OpenStore.Omnichannel.Shared.Request.ProductContext;

public record UpdateVariantBarcodesRequest(IEnumerable<UpdateVariantBarcodeRequest> Variants);

public record UpdateVariantBarcodeRequest(Guid VariantId, string Barcode);