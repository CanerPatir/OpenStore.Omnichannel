using System;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public abstract record DomainEventBase(Guid EntityId) : DomainEvent(EntityId.ToString());

    public record ProductCreated(Guid ProductId) : DomainEventBase(ProductId);

    public record ProductMediaCreated(
        Guid ProductMediaId,
        string Host,
        string Path,
        string Type,
        string Extension,
        string Filename,
        string Title,
        int Position,
        long? Size) : DomainEventBase(ProductMediaId);
}