using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Shared.Command.ProductContext;

public record CreateProductMedia(IEnumerable<FileUploadDto> Uploads) : ICommand<IEnumerable<ProductMediaDto>>;

public record AssignProductMedia(Guid Id, IEnumerable<FileUploadDto> Uploads) : ICommand<IEnumerable<ProductMediaDto>>;

public record CreateProduct(ProductDto Model) : ICommand<Guid>;

public record UpdateProduct(Guid ProductId, ProductDto Model) : ICommand;

public record UpdateProductVariant(Guid ProductId, Guid VariantId, VariantDto Model) : ICommand;

public record CreateVariant(Guid ProductId, VariantDto Model) : ICommand<Guid>;

public record UpdateProductVariantQuantities(Guid ProductId, IEnumerable<UpdateProductVariantQuantity> Variants) : ICommand;

public record UpdateProductVariantQuantity(Guid VariantId, int Quantity) : ICommand;

public record ArchiveProduct(Guid Id) : ICommand;

public record UnArchiveProduct(Guid Id) : ICommand;

public record DeleteProduct(Guid Id) : ICommand;

public record UpdateProductMedias(Guid Id, IEnumerable<ProductMediaDto> Medias) : ICommand;

public record DeleteProductMedia(Guid Id, Guid ProductMediaId) : ICommand;

public record UpdateProductVariantPrices(Guid ProductId, IEnumerable<UpdateProductVariantPrice> Variants) : ICommand;

public record UpdateProductVariantPrice(Guid VariantId, decimal Price, decimal? CompareAtPrice, decimal? Cost) : ICommand;

public record UpdateProductVariantBarcodes(Guid ProductId, IEnumerable<UpdateProductVariantBarcode> Variants) : ICommand;

public record UpdateProductVariantBarcode(Guid VariantId, string Barcode) : ICommand;

public record UpdateProductVariantSkus(Guid ProductId, IEnumerable<UpdateProductVariantSku> Variants) : ICommand;

public record UpdateProductVariantSku(Guid VariantId, string Sku) : ICommand;

public record DeleteVariants(Guid ProductId, IEnumerable<Guid> VariantIds) : ICommand;

public record MakeProductAsMultiVariant(Guid ProductId, IEnumerable<ProductOptionDto> Options, IEnumerable<VariantDto> Variants) : ICommand<IEnumerable<Guid>>;

public record ChangeVariantMedia(Guid ProductId, Guid VariantId, Guid MediaId) : ICommand;

// Product Collection

public record CreateProductCollection(ProductCollectionDto Model) : ICommand<Guid>;

public record UpdateProductCollection(Guid ProductCollectionId, ProductCollectionDto Model) : ICommand;

public record ChangeProductCollectionImage(Guid ProductCollectionId, FileUploadDto Model) : ICommand<ProductCollectionMediaDto>;

public record RemoveProductCollectionImage(Guid ProductCollectionId) : ICommand;

public record DeleteProductCollection(Guid ProductCollectionId) : ICommand;

public record RemoveProductCollectionItem(Guid ProductCollectionId, Guid ProductId) : ICommand;

public record AddItemsToCollection(Guid ProductCollectionId, IEnumerable<Guid> ProductIds) : ICommand;