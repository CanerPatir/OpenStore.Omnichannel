using MediatR;
using OpenStore.Omnichannel.Shared.Dto.Store;

namespace OpenStore.Omnichannel.Domain.StoreContext
{
    public record UpdateStorePreferences(StorePreferencesDto Model) : IRequest;
}