using MediatR;
using OpenStore.Omnichannel.Shared.Dto.Management.Inventory;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Shared.Query.Management.InventoryContext;

public record GetAllInventories(PageRequest PageRequest) : IRequest<PagedList<InventoryListItemDto>>;