using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application;
using OpenStore.Infrastructure.Messaging;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.ReadModel.Projections.Consumers
{
    public class OutBoxMessageConsumer :  IOpenStoreConsumer<MessageEnvelop>
    {
        private readonly IMediator _mediator;
        public OutBoxMessageConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
 
        public async Task Consume(MessageEnvelop message, CancellationToken cancellationToken)
        {
            var domainEvent = message.RecreateMessage();
            switch (domainEvent)
            {
                case ProductCreated:
                    break;
                case ProductDeleted:
                    break;
            }
        }
    }
}