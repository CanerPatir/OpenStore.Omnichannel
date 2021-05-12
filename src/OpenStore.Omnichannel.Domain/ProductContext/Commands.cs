using System;
using System.Collections.Generic;
using MediatR;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public record CreateProductMedia(IEnumerable<FileUploadDto> Uploads) : IRequest<IEnumerable<(ProductMediaDto dto, ProductMedia model)>>;

    public record CreateProduct(ProductDto Model) : IRequest<Guid>;
    
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
}