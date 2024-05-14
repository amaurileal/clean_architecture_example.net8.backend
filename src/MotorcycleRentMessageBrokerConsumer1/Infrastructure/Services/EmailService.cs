using MotorcycleRentMessageBrokerConsumer1.Domain;

namespace MotorcycleRentMessageBrokerConsumer1.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string message)
        {
            //Enviar Email
            Console.WriteLine("(Simulation) Email Sent {0}", message);
        }
    }
}
