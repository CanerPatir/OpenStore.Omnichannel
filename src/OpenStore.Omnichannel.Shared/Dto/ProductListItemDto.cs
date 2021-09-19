using System;

namespace OpenStore.Omnichannel.Shared.Dto
{
    public record ProductListItemDto(Guid Id,
        string PhotoUrl,
        ProductStatus Status,
        string Title,
        int? AvailableQuantity,
        bool HasMultipleVariants,
        int VariantCount,
        bool IsPhysicalProduct);
}