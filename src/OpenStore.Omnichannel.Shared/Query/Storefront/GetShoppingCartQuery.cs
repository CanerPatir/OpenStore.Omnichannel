using MediatR;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Shared.Query.Storefront;

public record GetShoppingCartQuery(Guid CartId): IRequest<ShoppingCartResult>;