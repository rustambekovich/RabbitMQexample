using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ResiverRabbitMQ.WebApi.Interfaces;
using System.Text;

namespace ResiverRabbitMQ.WebApi.Services.BackgroundServices;

public class CostumerService : BackgroundService
{
    private IFileService _fileService;
    private IConfiguration _config;
    private IModel _channel;

    public CostumerService(IFileService fileService,
                           IConfiguration configuration)
    {
        _fileService = fileService;
        _config = configuration.GetSection("MassageBroker");

        var factory = new ConnectionFactory()
        {
            HostName = _config["Host"],
            Port = int.Parse(_config["Port"]!),
            UserName = _config["Username"],
            Password = _config["Password"]
        };

        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();

        _channel.QueueDeclare(queue: _config["Queue"],
                              durable: false,
                              autoDelete: false,
                              exclusive: false,
                              arguments: null);

        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += Consumer_Received;
        _channel.BasicConsume(_config["Queue"], false, consumer);
        return Task.CompletedTask;
    }

    private void Consumer_Received(object? sender, BasicDeliverEventArgs model)
    {
        var jsonModel = Encoding.UTF8.GetString(model.Body.ToArray());
        _fileService.Write(jsonModel);
        _channel.BasicAck(deliveryTag: model.DeliveryTag, multiple: false);
    }

}
