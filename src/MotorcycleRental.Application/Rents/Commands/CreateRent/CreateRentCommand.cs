using MediatR;

namespace MotorcycleRental.Application.Rents.Commands.CreateRent
{
    public class CreateRentCommand : IRequest<int>
    {
        public int RentPlanId { get; set; } 
               
        public int MotorcycleId { get; set; } 

    }
}
