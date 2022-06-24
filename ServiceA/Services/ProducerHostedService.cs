using Common.Models;
using Common.RabbitMqComponents;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ServiceA.Services;

public class ProducerHostedService : BackgroundService
{
    private readonly IProducerService _producerService;

    public ProducerHostedService(
        IProducerService producerService
    )
    {
        _producerService = producerService;
    } 
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("Please enter your name: ");
            var name = Console.ReadLine();
            _producerService.SendMessage(name);
            
        }

        await Task.CompletedTask;
    }
}