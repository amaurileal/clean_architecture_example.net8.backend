using MotorcycleRental.Application.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MotorcycleRental.Infrastructure.MessageQueueService
{
    public class RabbitMQService : IMessageQueueService, IDisposable
    {
        private readonly RabbitMQ.Client.IModel channel;
        

        public RabbitMQService(string connectionString)
        {
           var factory = new ConnectionFactory() { Uri = new Uri(connectionString) };
            var connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
            this.channel.QueueDeclare(queue: "fila_moto2024",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            // configure exchanges, queues, bindings, etc.
        }

        public void Publish<T>(T message)
        {
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "",
                                 routingKey: "fila_moto2024",
                                 basicProperties: null,
                                 body: body);

            
            Console.WriteLine(($"sent message Rabbit MQ(fila_moto2024) {message}"));
            
        }

        public void Dispose()
        {
            channel?.Dispose();
        }
    }

}
