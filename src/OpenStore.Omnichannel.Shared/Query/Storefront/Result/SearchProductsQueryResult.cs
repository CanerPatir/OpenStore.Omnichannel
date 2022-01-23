namespace OpenStore.Omnichannel.Shared.Query.Storefront.Result;

public record SearchProductsQueryResult(IReadOnlyCollection<ProductItemDto> Items);