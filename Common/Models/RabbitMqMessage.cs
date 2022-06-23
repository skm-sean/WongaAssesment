namespace Common.Models;

public class RabbitMqMessage<T>
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); 
    public string? RoutingKey { get; set; }
    public T? Payload { get; set; }
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;
}