using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenStore.Infrastructure.Messaging.Kafka;
using OpenStore.Omnichannel.ReadModel.Projections.Consumers;

namespace OpenStore.Omnichannel.ReadModel.Projections
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddKafkaConsumer<Consumer, ProductMessage>("product-events", hostContext.Configuration.GetSection("Kafka"));

                    services.AddHostedService<TestWorker>();
                });
    }
}