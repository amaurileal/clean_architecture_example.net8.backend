using MediatR;
using MotorcycleRental.Application.Rents.Dtos;

namespace MotorcycleRental.Application.Motorcycles.Commands.CreateMotorcycle
{
    public class CreateMotorcycleCommand : IRequest<int>
    {
       public string Description { get; set; } = default!;
        public int Year { get; set; }

        public int Model { get; set; } = default!;

        public string LicensePlate { get; set; } = default!;

        public string Status { get; set; } = default!;

        
    }
}
