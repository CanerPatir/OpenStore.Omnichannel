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

        public override Task Handle(OutBoxMessage outBoxMessage, CancellationToken cancellationToken) =>
            _producer.Produce(ServiceCollectionExtensions.OpenStoreOutboxTopic,
                null,
                JsonSerializer.Serialize(outBoxMessage, new JsonSerializerOptions()), cancellationToken);
    }
}