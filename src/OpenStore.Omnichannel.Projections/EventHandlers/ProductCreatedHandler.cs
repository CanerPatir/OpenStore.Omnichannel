using OpenStore.Omnichannel.Shared.DomainEvents;
using OpenStore.Omnichannel.Shared.DomainEvents.ProductContext;

namespace OpenStore.Omnichannel.Projections.EventHandlers;

public class ProductCreatedHandler : DomainEventHandler<ProductCreated>
{
    protected override void Handle(ProductCreated notification)
    {
        throw new System.NotImplementedException();
    }
}