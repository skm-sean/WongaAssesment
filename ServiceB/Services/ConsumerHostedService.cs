using Common.RabbitMqComponents;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace ServiceB.Services;

public class ConsumerHostedService : RabbitMqConsumerBase, IHostedService
{
    private readonly ILogger _logger;

    public ConsumerHostedService(
        ILogger logger,
        ConnectionFactory connectionFactory
    ) : base(logger, connectionFactory)
    {
        _logger = logger;
        InitEvents();
    }

    private void InitEvents()
    {
        try
        {
            var consumer = new AsyncEventingBasicConsumer(Channel);
            consumer.Received += OnMessageReceived;
            Channel.BasicConsume(queue: "rabbitQue", autoAck: false, consumer: consumer); // Todo: get from config
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Unable to subscribe to que - {Que}", "rabbitQue"); // Todo: get from config
        }
    }

    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Dispose();
        return Task.CompletedTask;
    }
}