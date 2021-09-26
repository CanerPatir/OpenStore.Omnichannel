using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenStore.Application;
using OpenStore.Data;
using OpenStore.Infrastructure.Messaging.Kafka;
using OpenStore.Omnichannel.ReadModel.Projections.Consumers;

namespace OpenStore.Omnichannel.ReadModel.Projections
{
    public static class ServiceCollectionExtensions
    {
        internal const string OpenStoreOutboxTopic = "open-store-outbox-topic";

        public static IServiceCollection AddProjections(this IServiceCollection services, IConfigurationSection kafkaConfigSection)
        {
            services.AddHostedService<OutBoxPollHost>();
            
            services.AddKafkaProducer(kafkaConfigSection);
            services.AddKafkaConsumer<OutBoxMessageConsumer, MessageEnvelop>(OpenStoreOutboxTopic, kafkaConfigSection);

            // services.AddHostedService<TestWorker>();
            return services;
        }
    }
}