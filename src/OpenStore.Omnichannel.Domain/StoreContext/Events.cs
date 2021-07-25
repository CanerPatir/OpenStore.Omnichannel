using System;
using OpenStore.Omnichannel.Shared.Dto.Store;

namespace OpenStore.Omnichannel.Domain.StoreContext
{
    public record StorePreferencesUpdated(Guid EntityId, StorePreferencesDto Model) : DomainEventBase(EntityId);
}