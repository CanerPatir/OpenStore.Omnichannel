namespace OpenStore.Omnichannel.Shared.Dto.Management.Product;

public record ProductListItemDto(Guid Id,
    string PhotoUrl,
    ProductStatus Status,
    string Title,
    int? AvailableQuantity,
    bool HasMultipleVariants,
    int VariantCount,
    bool IsPhysicalProduct);