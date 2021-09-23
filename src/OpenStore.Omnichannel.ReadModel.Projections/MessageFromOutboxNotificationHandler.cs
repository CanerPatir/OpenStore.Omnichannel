using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OpenStore.Application;
using OpenStore.Infrastructure.Messaging.Kafka;

namespace OpenStore.Omnichannel.ReadModel.Projections
{
    public class MessageFromOutboxNotificationHandler : MediatR.INotificationHandler<MessageEnvelop>
    {
        private readonly KafkaProducer _producer;

        public MessageFromOutboxNotificationHandler(KafkaProducer producer)
        {
            _producer = producer;
        }

        public Task Handle(MessageEnvelop message, CancellationToken cancellationToken) => _producer.Produce(ServiceCollectionExtensions.OpenStoreOutboxTopic, null,
            JsonSerializer.Serialize(message, new JsonSerializerOptions()), cancellationToken);
    }
}