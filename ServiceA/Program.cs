using Common.Models;
using Common.RabbitMqComponents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Serilog;
using ServiceA.Services;
using ILogger = Serilog.ILogger;

var host = Host.CreateDefaultBuilder();

host.ConfigureServices((context, services) =>
{
    services.AddHostedService<ProducerHostedService>();
    
    services.AddSingleton<RabbitMqProducerBase<RabbitMqMessage<PayloadBase>>>();
    services.AddSingleton<IProducerService, ProducerService>();
    services.AddSingleton<ILogger>(new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger());

    services.AddSingleton(new ConnectionFactory
    {
        Uri = new Uri("amqp://guest:guest@localhost:5672")
    });
});

host.Build().Run();