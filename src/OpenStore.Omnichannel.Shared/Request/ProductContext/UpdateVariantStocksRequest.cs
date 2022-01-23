namespace OpenStore.Omnichannel.Shared.Request.ProductContext;

public record UpdateVariantStocksRequest(IEnumerable<UpdateVariantStockRequest> Variants);

public record UpdateVariantStockRequest(Guid VariantId, int Quantity);