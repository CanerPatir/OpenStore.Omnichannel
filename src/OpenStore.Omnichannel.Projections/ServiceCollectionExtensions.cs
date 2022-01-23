using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OpenStore.Omnichannel.Projections;

public static class ServiceCollectionExtensions
{
    internal const string OpenStoreOutboxTopic = "open-store-outbox-topic";

    public static IServiceCollection AddProjections(
        this IServiceCollection services
        , IConfigurationSection kafkaConfigSection
        , IConfigurationSection elasticSearchConfigSection)
    {
        // services.AddKafkaProducer(kafkaConfigSection);
        // services.AddKafkaConsumer<OutBoxMessageConsumer, OutBoxMessage>(OpenStoreOutboxTopic, kafkaConfigSection);
        // services.AddElasticSearch(elasticSearchConfigSection);
        //// services.AddHostedService<TestWorker>();

        // Todo: run on app init
        // using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build())
        // {
        //     try
        //     {
        //          adminClient.CreateTopicsAsync(new[] { 
        //             new TopicSpecification { Name = OpenStoreOutboxTopic, ReplicationFactor = 2, NumPartitions = 20 } })
        //              .ConfigureAwait(false).GetAwaiter().GetResult();
        //     }
        //     catch (CreateTopicsException e)
        //     {
        //         Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
        //     }
        // }
            
        return services;
    }
}