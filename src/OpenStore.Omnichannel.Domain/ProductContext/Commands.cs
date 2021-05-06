using System;
using System.Collections.Generic;
using MediatR;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public record CreateProductMedia(IEnumerable<FileUploadDto> Uploads) : IRequest<IEnumerable<ProductMediaDto>>;

    public record CreateProduct(ProductDto Model) : IRequest<Guid>;

    public record UpdateProductVariantQuantity(Guid VariantId, int Quantity);

}