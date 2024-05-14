using MotorcycleRentMessageBrokerConsumer1.Domain;
using MotorcycleRentMessageBrokerConsumer1.Domain.Entities;
using MotorcycleRentMessageBrokerConsumer1.Domain.Repositories;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Serilog;
using Microsoft.Extensions.Logging;

namespace MotorcycleRentMessageBrokerConsumer1.Infrastructure.Services
{
    public class RabbitMQConsumer
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private readonly IMotorcycle2024Repository _motorcycle2024Repository;
        private readonly IEmailService _emailService;
        private readonly ILogger<RabbitMQConsumer> _logger;

        public RabbitMQConsumer(IMotorcycle2024Repository motorcycle2024Repository, IEmailService emailService, ILogger<RabbitMQConsumer> logger,string hostname, string queueName, string username, string password)
        {
            _hostname = hostname;
            _queueName = queueName;
            _username = username;
            _password = password;
            _emailService = emailService;
            _motorcycle2024Repository = motorcycle2024Repository;
            _logger = logger;
        }

        public void Start()
        {
            var factory = new ConnectionFactory() { HostName = _hostname, UserName = _username, Password = _password };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received: {0}", message);
                _logger.LogInformation("Received: {0}", message);
                // Process the message received here
                var newMotorcycle = JsonConvert.DeserializeObject<Motorcycle2024>(message);

                newMotorcycle.CreateDate = DateTime.UtcNow;

                if (newMotorcycle.Year == 2024)
                {
                    _emailService.Send($"Motocycle 2024 Inserted! {message}");
                }

                //Insert into DB
                await _motorcycle2024Repository.Create(newMotorcycle);

                // Message Confimation to RabbitMQ
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: _queueName,
                                 autoAck: false,
                                 consumer: consumer);

            Console.WriteLine("Press [enter] to exit.");
            _logger.LogInformation("Consumer started, listen publisher...");
            Console.ReadLine();
        }
    }
}
