using OpenStore.Omnichannel.Shared.Dto.Store;

// ReSharper disable once CheckNamespace
namespace OpenStore.Omnichannel.Domain.StoreContext;

public record StorePreferencesUpdated(Guid EntityId, StorePreferencesDto Model) : DomainEventBase(EntityId);