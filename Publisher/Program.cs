using RabbitMQ.Client;
using System.Text;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };//instance for conneting t server. "localhost" indicates that its working locally
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "dev-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var message = "Hi from Publisher";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "dev-queue",
                                     basicProperties: null,
                                     body: body);

                System.Console.WriteLine("Mess is sent into Default Ex");
            }
        }
    }
}
