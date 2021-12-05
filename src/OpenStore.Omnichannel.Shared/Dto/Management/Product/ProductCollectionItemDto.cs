namespace OpenStore.Omnichannel.Shared.Dto.Management.Product;

public record ProductCollectionItemDto(Guid ProductId, string Title, string PhotoUrl)
{
    public bool Selected { get; set; }
};