using System;
using MediatR;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Application.Command
{
    public record CreateProduct(ProductDto Model) : IRequest<Guid>;
}