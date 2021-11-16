namespace OpenStore.Omnichannel.Shared.Query.Result;

public record ProductDetailResult(
    string Handle,
    Guid Id,
    string Title,
    string Description,
    string MetaTitle,
    string MetaDescription,
    bool HasMultipleVariants,
    bool IsPhysicalProduct,
    IReadOnlyCollection<ProductDetailMediaDto> Medias,
    IReadOnlyCollection<ProductDetailVariantDto> Variants
);

public record class ProductDetailMediaDto(
    Guid Id,
    string Host,
    string Path,
    string Type,
    string Title,
    string Extension,
    string Filename,
    int Position,
    long? Size,
    string Url,
    HashSet<Guid> VariantIds
);

public record class ProductDetailVariantDto(
    Guid Id,
    decimal Price,
    decimal? CompareAtPrice,
    bool CalculateTaxAdditionally,
    string Sku,
    string Barcode,
    bool TrackQuantity,
    bool ContinueSellingWhenOutOfStock,
    int Quantity,
    string Option1,
    string Option2,
    string Option3,
    string Title
);