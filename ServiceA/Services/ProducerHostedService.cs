using Common.Models;
using Common.RabbitMqComponents;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ServiceA.Services;

public class ProducerHostedService : BackgroundService
{
    
    private readonly RabbitMqProducerBase<RabbitMqMessage<PayloadBase>> _producer;
    private readonly ILogger _logger;

    public ProducerHostedService(
        RabbitMqProducerBase<RabbitMqMessage<PayloadBase>> producer,
        ILogger logger
    )
    {
        _producer = producer;
        _logger = logger;
    } 
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("Please enter your name: ");
            var name = Console.ReadLine();
            var message = $"Hello my name is, {name}";
            PublishMessage(message, name);
            Console.WriteLine($"Hello {name}!");
        }

        await Task.CompletedTask;
    }

    

    private void PublishMessage(string? message, string? name)
    {
        var payload = new PayloadBase
        {
            Message = message,
            Name = name
        };

        var rabbitMqPayload = new RabbitMqMessage<PayloadBase>
        {
            Payload = payload,
        };
        
        _producer.Publish(rabbitMqPayload);
    }
}