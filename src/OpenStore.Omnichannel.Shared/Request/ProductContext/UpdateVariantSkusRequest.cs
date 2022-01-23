namespace OpenStore.Omnichannel.Shared.Request.ProductContext;

public record UpdateVariantSkusRequest(IEnumerable<UpdateVariantSkuRequest> Variants);

public record UpdateVariantSkuRequest(Guid VariantId, string Sku);