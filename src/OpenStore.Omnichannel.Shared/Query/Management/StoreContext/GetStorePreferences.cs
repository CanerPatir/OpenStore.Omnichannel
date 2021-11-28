using MediatR;
using OpenStore.Omnichannel.Shared.Dto.Management.Store;

namespace OpenStore.Omnichannel.Shared.Query.Management.StoreContext;

public record GetStorePreferences : IRequest<StorePreferencesDto>;
