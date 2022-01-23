using MediatR;

namespace OpenStore.Omnichannel.Shared.DomainEvents;

/// <summary>
/// Represents domain event handlers
/// </summary>
/// <typeparam name="TDomainEvent"></typeparam>
public abstract class DomainEventHandler<TDomainEvent> : NotificationHandler<TDomainEvent>
    where TDomainEvent : DomainEventBase
{
}