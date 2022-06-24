using System.Text;
using System.Text.Json;
using Common.Models;
using Common.Validators;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace Common.RabbitMqComponents;

public class RabbitMqConsumerBase : RabbitMqBase
{
    private readonly ILogger _logger;
    protected RabbitMqConsumerBase(
        ILogger logger, 
        ConnectionFactory connectionFactory) : base(logger, connectionFactory)
    {
        _logger = logger;
    }
    
    protected Task OnMessageReceived(object sender, BasicDeliverEventArgs args)
    {
        try
        {
            var messageBody = Encoding.UTF8.GetString(args.Body.ToArray());
            var message = JsonSerializer.Deserialize<RabbitMqMessage<PayloadBase>>(messageBody);

            var validator = new PayLoadBaseValidator();
            var validationResult = validator.Validate(message.Payload);
            
            if (!validationResult.IsValid)
            {
                _logger.Error(
                    "Invalid message received: {Message}",
                    validationResult.ToString()
                );
                return Task.CompletedTask;
            }
            
            Console.WriteLine($"Hello {message.Payload?.Name}, I am your father!");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Unable to receive message from que - {RoutingKey}", args.RoutingKey);
        }
        finally
        {
            Channel?.BasicAck(args.DeliveryTag, false);
        }

        return Task.CompletedTask;
    }
}