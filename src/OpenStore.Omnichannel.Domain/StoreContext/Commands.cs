using MediatR;
using OpenStore.Omnichannel.Shared.Dto.Management.Store;

namespace OpenStore.Omnichannel.Domain.StoreContext;

public record UpdateStorePreferences(StorePreferencesDto Model) : IRequest;