using RabbitMQ.Client;
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
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Console.Write("Enter massage: ");
            string message = Console.ReadLine()!;
            var request = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "first",
                                 basicProperties: null,
                                 body: request);



            Console.WriteLine("Massage send rabbitMQ");
        }

        Main(args);
    }
}


