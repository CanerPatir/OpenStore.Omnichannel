using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OpenStore.Omnichannel.Projections;

public class TestWorker : BackgroundService
{
    private readonly ILogger<TestWorker> _logger;

    public TestWorker(ILogger<TestWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogError("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}