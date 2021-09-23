using System.Threading;
using System.Threading.Tasks;
using OpenStore.Application;
using OpenStore.Infrastructure.Messaging;

namespace OpenStore.Omnichannel.ReadModel.Projections.Consumers
{
    public class OutBoxMessageConsumer :  IOpenStoreConsumer<MessageEnvelop>
    {
        public OutBoxMessageConsumer()
        {
        }
 
        public Task Consume(MessageEnvelop message, CancellationToken cancellationToken)
        {
            var domainEvent = message.RecreateMessage();
            if (domainEvent is )
            {
                
            }
 
        }
    }
}