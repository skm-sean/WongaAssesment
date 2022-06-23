using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Serilog;

namespace Common.RabbitMqComponents;

public sealed class RabbitMqProducerBase<T> : RabbitMqBase
{
    private readonly ILogger _logger;

    public RabbitMqProducerBase(
        ConnectionFactory connectionFactory,
        ILogger logger
    ) : base(logger, connectionFactory)
    {
        _logger = logger;
    }
    
    public void Publish(T args)
    {
        try
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(args));
            Channel.BasicPublish("", "rabbitQue", null, body);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Unable to publish message to que - {Que}", "rabbitQue");
        }
        
    }
}