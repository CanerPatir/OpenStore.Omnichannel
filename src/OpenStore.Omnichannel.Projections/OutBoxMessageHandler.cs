using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OpenStore.Data.OutBox;
using OpenStore.Infrastructure.Messaging.Kafka;

namespace OpenStore.Omnichannel.Projections
{
    public class OutBoxMessageHandler : BaseOutBoxMessageHandler
    {
        private readonly KafkaProducer _producer;

        public OutBoxMessageHandler(KafkaProducer producer)
        {
            _producer = producer;
        }

        // todo: monitor performance
        public override async Task Handle(OutBoxMessageBatch outBoxMessageBatch, CancellationToken cancellationToken)
        {
            foreach (var outBoxMessagesByAggregateId in outBoxMessageBatch.Messages.GroupBy(x => x.AggregateId))
            {
                await _producer.ProduceMany(ServiceCollectionExtensions.OpenStoreOutboxTopic,
                    outBoxMessagesByAggregateId.Key, 
                    outBoxMessagesByAggregateId.ToList(),
                    cancellationToken);
            }
        }
    }
}