using MediatR;
using OpenStore.Data.OutBox;
using OpenStore.Infrastructure.Messaging;
using OpenStore.Omnichannel.Shared.DomainEvents.ProductContext;

namespace OpenStore.Omnichannel.Projections;

public class OutBoxMessageConsumer : IOpenStoreConsumer<OutBoxMessage>
{
    private readonly IMediator _mediator;

    public OutBoxMessageConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    // todo: Monitor performance
    public async Task Consume(OutBoxMessage message, CancellationToken cancellationToken)
    {
        var domainEvent = message.RecreateMessage();
        switch (domainEvent)
        {
            case ProductCreated productCreated:
                await _mediator.Publish(productCreated, cancellationToken);
                break;
            case ProductDeleted productDeleted:
                await _mediator.Publish(productDeleted, cancellationToken);
                break;
        }
        // todo: other events
    }
}