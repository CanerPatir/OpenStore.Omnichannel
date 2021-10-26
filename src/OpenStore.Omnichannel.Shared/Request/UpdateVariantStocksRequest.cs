namespace OpenStore.Omnichannel.Shared.Request;

public record UpdateVariantStocksRequest(IEnumerable<UpdateVariantStockRequest> Variants);

public record UpdateVariantStockRequest(Guid VariantId, int Quantity);