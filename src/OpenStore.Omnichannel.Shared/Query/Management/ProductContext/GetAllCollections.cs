using MediatR;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Shared.Query.Management.ProductContext;

public record GetAllCollections(PageRequest PageRequest) : IRequest<PagedList<CollectionListItemDto>>;