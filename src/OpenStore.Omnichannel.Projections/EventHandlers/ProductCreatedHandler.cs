using MediatR;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Projections.EventHandlers
{
    public class ProductCreatedHandler : NotificationHandler<ProductCreated>
    {
        protected override void Handle(ProductCreated notification)
        {
            throw new System.NotImplementedException();
        }
    }
}