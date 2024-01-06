using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SenderRabbitMQ.WebApi.Service.Dto;
using SenderRabbitMQ.WebApi.Service.Interfaces;
using System.Text;

namespace SenderRabbitMQ.WebApi.Service.ProducerServices;

public class ProducerService : IProducerService
{
    private IConfiguration _config;
    private readonly IModel _channel;
    public ProducerService(IConfiguration configuration)
    {
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
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
    }
    [Benchmark]
    public void Send(Massage massage)
    {
        var JsonMassage = JsonConvert.SerializeObject(massage);

        var body = Encoding.UTF8.GetBytes(JsonMassage);

        _channel.BasicPublish(exchange: "",
                              routingKey: _config["Queue"],
                              basicProperties: null,
                              body: body);
    }
}
