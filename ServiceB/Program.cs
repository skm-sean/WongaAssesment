using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Serilog;
using ServiceB.Services;

var host = Host.CreateDefaultBuilder();

host.ConfigureServices((context, services) =>
{
    services.AddHostedService<ConsumerHostedService>();
    services.AddSingleton<ILogger>(new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger());
    services.AddSingleton(new ConnectionFactory
    {
        Uri = new Uri("amqp://guest:guest@localhost:5672"), // Todo: get from config
        DispatchConsumersAsync = true
    });
});

host.Build().Run();