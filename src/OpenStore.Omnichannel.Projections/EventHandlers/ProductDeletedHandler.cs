using MediatR;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Projections.EventHandlers
{
    public class ProductDeletedHandler: NotificationHandler<ProductDeleted>
    {
        protected override void Handle(ProductDeleted notification)
        {
            throw new System.NotImplementedException();
        }
    }
}