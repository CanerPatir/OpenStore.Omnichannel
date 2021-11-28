namespace OpenStore.Omnichannel.Shared.Query.Storefront.Result;

public record ProductItemDto(Guid Id, string Title, string PhotoUrl, string Handle, decimal Price, decimal? CompareAtPrice);