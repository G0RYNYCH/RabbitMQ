using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {

            var counter = 0;

            do
            {
                var timeToSleep = new Random().Next(1000, 3000);
                Thread.Sleep(timeToSleep);

                var factory = new ConnectionFactory() { HostName = "localhost" };//instance for conneting to server. "localhost" indicates that its working locally
                using (var connection = factory.CreateConnection()) //manage the version of protocol, auth, ...
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "my-queue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var message = $"Mess from Publisher [#{counter}]";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "my-queue",
                                         basicProperties: null,
                                         body: body);

                    System.Console.WriteLine($"Mess is sent into Default Exchange [#{counter++}]");
                }
            }

            while (true);
        }
    }
}
