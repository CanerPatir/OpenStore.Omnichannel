using OpenStore.Domain;

namespace OpenStore.Omnichannel.Shared.DomainEvents;

/// <summary>
/// Represents Open Store domain events
/// </summary>
/// <param name="EntityId">Aggregate id</param>
public abstract record DomainEventBase(Guid EntityId) : DomainEvent(EntityId.ToString());