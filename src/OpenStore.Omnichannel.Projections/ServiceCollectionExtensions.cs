using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenStore.Data.OutBox;
using OpenStore.Infrastructure.Messaging.Kafka;

namespace OpenStore.Omnichannel.Projections
{
    public static class ServiceCollectionExtensions
    {
        internal const string OpenStoreOutboxTopic = "open-store-outbox-topic";

        public static IServiceCollection AddProjections(this IServiceCollection services, IConfigurationSection kafkaConfigSection)
        {
            services.AddKafkaProducer(kafkaConfigSection);
            services.AddKafkaConsumer<OutBoxMessageConsumer, OutBoxMessage>(OpenStoreOutboxTopic, kafkaConfigSection);
            // services.AddHostedService<TestWorker>();
            
            return services;
        }
    }
}