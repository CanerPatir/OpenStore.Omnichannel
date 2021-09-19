using System.Threading;
using System.Threading.Tasks;
using OpenStore.Application;
using OpenStore.Infrastructure.Messaging;

namespace OpenStore.Omnichannel.ReadModel.Projections.Consumers
{
    public class ProductMessage : MessageEnvelop
    {
    }

    public class ListingMessage : MessageEnvelop
    {
    }

    public class Consumer :
        IOpenStoreConsumer<ProductMessage>,
        IOpenStoreConsumer<ListingMessage>
    {
        public Consumer()
        {
        }

        public async Task Consume(ProductMessage message, CancellationToken cancellationToken)
        {
            // await _elasticSearchStore.Index<ProductDocument>(, cancellationToken: cancellationToken)
            throw new System.NotImplementedException();
        }

        public Task Consume(ListingMessage message, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}