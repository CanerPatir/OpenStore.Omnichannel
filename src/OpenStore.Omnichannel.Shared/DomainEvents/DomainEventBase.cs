using OpenStore.Domain;

// ReSharper disable once CheckNamespace
namespace OpenStore.Omnichannel.Domain;

public abstract record DomainEventBase(Guid EntityId) : DomainEvent(EntityId.ToString());