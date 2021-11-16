namespace OpenStore.Omnichannel.Shared.Query.Result;

public record ProductItemDto(Guid Id, string Title, string PhotoUrl, string Handle, decimal Price, decimal? CompareAtPrice);