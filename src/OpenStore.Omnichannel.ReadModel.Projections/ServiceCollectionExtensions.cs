using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenStore.Infrastructure.Data;
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
            services.AddKafkaConsumer<Consumer, ProductMessage>(OpenStoreOutboxTopic, kafkaConfigSection);

            // services.AddHostedService<TestWorker>();
            return services;
        }
    }
}