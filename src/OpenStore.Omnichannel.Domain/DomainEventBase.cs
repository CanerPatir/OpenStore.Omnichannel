using System;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain
{
    public abstract record DomainEventBase(Guid EntityId) : DomainEvent(EntityId.ToString());
}