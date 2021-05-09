using System;
using System.Collections.Generic;
using MediatR;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public record CreateProductMedia(IEnumerable<FileUploadDto> Uploads) : IRequest<IEnumerable<(ProductMediaDto dto, ProductMedia model)>>;

    public record CreateProduct(ProductDto Model) : IRequest<Guid>;

    public record UpdateProductVariantQuantity(Guid VariantId, int Quantity) : IRequest;

    public record ArchiveProduct(Guid Id) : IRequest;

    public record UnArchiveProduct(Guid Id) : IRequest;

    public record DeleteProduct(Guid Id) : IRequest;

    public record UpdateProductMedias(Guid Id, IEnumerable<ProductMediaDto> Medias) : IRequest;

    public record DeleteProductMedia(Guid Id, Guid ProductMediaId) : IRequest;
}