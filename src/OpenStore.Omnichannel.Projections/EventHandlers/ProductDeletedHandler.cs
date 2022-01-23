using OpenStore.Omnichannel.Shared.DomainEvents;
using OpenStore.Omnichannel.Shared.DomainEvents.ProductContext;

namespace OpenStore.Omnichannel.Projections.EventHandlers;

public class ProductDeletedHandler: DomainEventHandler<ProductDeleted>
{
    protected override void Handle(ProductDeleted notification)
    {
        throw new System.NotImplementedException();
    }
}