using MediatR;

namespace MotorcycleRental.Application.Motorcycles.Commands.DeleteMotorcycle
{
    public class DeleteMotorcycleCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
