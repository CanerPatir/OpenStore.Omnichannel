namespace OpenStore.Omnichannel.Shared.Query.Storefront.Result;

public record SearchProductsResult(IReadOnlyCollection<ProductItemDto> Items);