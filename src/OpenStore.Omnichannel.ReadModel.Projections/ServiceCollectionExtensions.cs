using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenStore.Infrastructure.Data;
using OpenStore.Infrastructure.Messaging.Kafka;
using OpenStore.Omnichannel.ReadModel.Projections.Consumers;

namespace OpenStore.Omnichannel.ReadModel.Projections
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjections(this IServiceCollection services, IConfigurationSection kafkaConfigSection)
        {
            services.AddHostedService<OutBoxPollHost>();
            
            services.AddKafkaProducer(kafkaConfigSection);
            services.AddKafkaConsumer<Consumer, ProductMessage>("open-store-outbox-topic", kafkaConfigSection);

            // services.AddHostedService<TestWorker>();
            return services;
        }
    }
}