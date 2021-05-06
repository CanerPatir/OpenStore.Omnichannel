using System;

namespace OpenStore.Omnichannel.Shared.ReadModel
{
    public record ProductListItemReadModel(Guid Id,
        string PhotoUrl,
        ProductStatus Status, 
        string Title,
        int? AvailableQuantity,
        bool HasMultipleVariants,
        int VariantCount, 
        bool IsPhysicalProduct);
}