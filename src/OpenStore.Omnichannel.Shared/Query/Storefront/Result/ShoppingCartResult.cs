namespace OpenStore.Omnichannel.Shared.Query.Storefront.Result;

public record ShoppingCartResult(Guid Id, bool IsAuthenticated, IReadOnlyCollection<ShoppingCartItemDto> Items);

public record ShoppingCartItemDto(Guid Id, Guid VariantId, int Quantity);