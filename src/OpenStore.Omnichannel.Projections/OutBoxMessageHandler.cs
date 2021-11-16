using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenStore.Data.OutBox;

namespace OpenStore.Omnichannel.Projections;

public class OutBoxMessageHandler : BaseOutBoxMessageHandler
{
    // private readonly KafkaProducer _producer;
    private readonly ILogger<OutBoxMessageHandler> _logger;

    public OutBoxMessageHandler(
        // KafkaProducer producer,
        ILogger<OutBoxMessageHandler> logger
    )
    {
        // _producer = producer;
        _logger = logger;
    }

    // todo: monitor performance
    public override async Task Handle(OutBoxMessageBatch outBoxMessageBatch, CancellationToken cancellationToken)
    {
        foreach (var outBoxMessagesByAggregateId in outBoxMessageBatch.Messages.GroupBy(x => x.AggregateId))
        {
            _logger.LogInformation($"{outBoxMessageBatch.Messages.Count} retrieved");
            await Task.Delay(10, cancellationToken);
            // await _producer.ProduceMany(ServiceCollectionExtensions.OpenStoreOutboxTopic,
            //     outBoxMessagesByAggregateId.Key, 
            //     outBoxMessagesByAggregateId.ToList(),
            //     cancellationToken);
        }
    }
}