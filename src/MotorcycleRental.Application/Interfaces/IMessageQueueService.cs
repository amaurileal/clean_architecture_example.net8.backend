namespace MotorcycleRental.Application.Interfaces
{
    public interface IMessageQueueService
    {
        void Publish<T>(T message);
    }
}
