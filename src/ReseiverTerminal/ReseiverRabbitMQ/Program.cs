using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "first",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var response = new EventingBasicConsumer(channel);

            response.Received += (model, convertToString) =>
            {
                var body = convertToString.Body.ToArray();
                var massage = Encoding.UTF8.GetString(body);

                Console.WriteLine("RabbitMQ massage: " + massage);
            };

            channel.BasicConsume(queue: "first",
                                 autoAck: true,
                                 consumer: response);

            Console.ReadLine();
        }

    }
}