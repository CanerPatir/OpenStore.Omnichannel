namespace OpenStore.Omnichannel.Shared.Request.ProductContext;

public record UpdateVariantPricesRequest(IEnumerable<UpdateVariantPriceRequest> Variants);

public record UpdateVariantPriceRequest(Guid VariantId, decimal Price, decimal? CompareAtPrice, decimal? Cost);