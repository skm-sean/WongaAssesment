using RabbitMQ.Client;
using Serilog;

namespace Common.RabbitMqComponents;

public class RabbitMqBase : IDisposable
{
    private readonly ILogger _logger;
    private readonly ConnectionFactory _connectionFactory;

    protected IModel? Channel { get; set; }
    private IConnection? _connection;
    
    protected RabbitMqBase(
        ILogger logger,
        ConnectionFactory connectionFactory)
    {
        _logger = logger;
        _connectionFactory = connectionFactory;
        ConnectToRabbitMq();
    }

    private void ConnectToRabbitMq()
    {
        if (_connection is not {IsOpen: true})
        {
            _connection = _connectionFactory.CreateConnection();
        }
        
        _connection.ConnectionShutdown += ConnectionOnConnectionShutdown;

        if (Channel is {IsOpen: true}) 
            return;
        
        Channel = _connection.CreateModel();
        Channel.QueueDeclare(
            queue: "rabbitQue", // Todo: get from config
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    private void ConnectionOnConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        //Todo: handle connection shutdown and create a full reconnection
    }
    
    public void Dispose()
    {
        try
        {
            Channel?.Close();
            Channel?.Dispose();
            Channel = null;
            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Unable to dispose of {Class}", nameof(RabbitMqBase));
        }
    }
}