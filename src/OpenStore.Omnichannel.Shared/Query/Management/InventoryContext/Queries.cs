using OpenStore.Omnichannel.Shared.Query.Management.InventoryContext.Result;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Shared.Query.Management.InventoryContext;

public record GetAllInventoriesQuery(PageRequest PageRequest) : IQuery<PagedList<InventoryListItemDto>>;