using Microsoft.Extensions.Hosting;

namespace ServiceA.Services;

public class ProducerHostedService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: Implement ConsoleListener
        
        return Task.CompletedTask;
    }
}