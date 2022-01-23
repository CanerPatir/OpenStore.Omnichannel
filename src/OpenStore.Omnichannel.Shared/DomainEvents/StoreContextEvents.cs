using OpenStore.Omnichannel.Shared.Query.Management.StoreContext.Result;

// ReSharper disable once CheckNamespace
namespace OpenStore.Omnichannel.Shared.DomainEvents.StoreContext;

public record StorePreferencesUpdated(Guid EntityId, StorePreferencesQueryResult Model) : DomainEventBase(EntityId);