using Common.Models;
using Common.RabbitMqComponents;
using Common.Validators;
using Serilog;

namespace ServiceA.Services;

public interface IProducerService
{
    void SendMessage(string? message);
}

public class ProducerService : IProducerService
{
    private readonly RabbitMqProducerBase<RabbitMqMessage<PayloadBase>> _producer;
    private readonly ILogger _logger;
    
    public ProducerService(
        RabbitMqProducerBase<RabbitMqMessage<PayloadBase>> producer,
        ILogger logger)
    {
        _producer = producer;
        _logger = logger;
    }
    
    public void SendMessage(string? name)
    {
        var message = $"Hello my name is, {name}";
        
        var payload = new PayloadBase
        {
            Message = message,
            Name = name
        };
        
        if(ValidatePayload(payload))
            PublishMessage(payload);
    }
    
    private void PublishMessage(PayloadBase payloadBase)
    {
        var rabbitMqPayload = new RabbitMqMessage<PayloadBase>
        {
            Payload = payloadBase,
        };
        
        _producer.Publish(rabbitMqPayload);
    }

    private bool ValidatePayload(PayloadBase payload)
    {
        var validator = new PayLoadBaseValidator();
        var result = validator.Validate(payload);

        if (result.IsValid) 
            return true;
        
        foreach (var error in result.Errors)
        {
            _logger.Error("{Error}", error.ErrorMessage);
        }
            
        return false;
    }
}