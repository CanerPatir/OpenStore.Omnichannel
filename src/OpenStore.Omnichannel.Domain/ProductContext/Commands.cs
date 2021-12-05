using MediatR;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Domain.ProductContext;

public record CreateProductMedia(IEnumerable<FileUploadDto> Uploads) : IRequest<IEnumerable<(ProductMediaDto dto, ProductMedia model)>>;

public record AssignProductMedia(Guid Id, IEnumerable<FileUploadDto> Uploads) : IRequest<IEnumerable<ProductMediaDto>>;

public record CreateProduct(ProductDto Model) : IRequest<Guid>;

public record UpdateProduct(Guid ProductId, ProductDto Model) : IRequest;

public record UpdateProductVariant(Guid ProductId, Guid VariantId, VariantDto Model) : IRequest;

public record CreateVariant(Guid ProductId, VariantDto Model) : IRequest<Guid>;

public record UpdateProductVariantQuantities(Guid ProductId, IEnumerable<UpdateProductVariantQuantity> Variants) : IRequest;

public record UpdateProductVariantQuantity(Guid VariantId, int Quantity) : IRequest;

public record ArchiveProduct(Guid Id) : IRequest;

public record UnArchiveProduct(Guid Id) : IRequest;

public record DeleteProduct(Guid Id) : IRequest;

public record UpdateProductMedias(Guid Id, IEnumerable<ProductMediaDto> Medias) : IRequest;

public record DeleteProductMedia(Guid Id, Guid ProductMediaId) : IRequest;

public record UpdateProductVariantPrices(Guid ProductId, IEnumerable<UpdateProductVariantPrice> Variants) : IRequest;

public record UpdateProductVariantPrice(Guid VariantId, decimal Price, decimal? CompareAtPrice, decimal? Cost) : IRequest;

public record UpdateProductVariantBarcodes(Guid ProductId, IEnumerable<UpdateProductVariantBarcode> Variants) : IRequest;

public record UpdateProductVariantBarcode(Guid VariantId, string Barcode) : IRequest;

public record UpdateProductVariantSkus(Guid ProductId, IEnumerable<UpdateProductVariantSku> Variants) : IRequest;

public record UpdateProductVariantSku(Guid VariantId, string Sku) : IRequest;

public record DeleteVariants(Guid ProductId, IEnumerable<Guid> VariantIds) : IRequest;

public record MakeProductAsMultiVariant(Guid ProductId, IEnumerable<ProductOptionDto> Options, IEnumerable<VariantDto> Variants) : IRequest<IEnumerable<Guid>>;

public record ChangeVariantMedia(Guid ProductId, Guid VariantId, Guid MediaId) : IRequest;

// Product Collection

public record CreateProductCollection(ProductCollectionDto Model) : IRequest<Guid>;

public record UpdateProductCollection(Guid ProductCollectionId, ProductCollectionDto Model) : IRequest;

public record ChangeProductCollectionImage(Guid ProductCollectionId, FileUploadDto Model) : IRequest<ProductCollectionMediaDto>;

public record RemoveProductCollectionImage(Guid ProductCollectionId) : IRequest;

public record DeleteProductCollection(Guid ProductCollectionId) : IRequest;

public record RemoveProductCollectionItem(Guid ProductCollectionId, Guid ProductId) : IRequest;

public record AddItemsToCollection(Guid ProductCollectionId, IEnumerable<Guid> ProductIds) : IRequest;