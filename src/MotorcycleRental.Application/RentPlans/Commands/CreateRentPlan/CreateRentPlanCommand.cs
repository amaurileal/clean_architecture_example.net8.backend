using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Application.RentPlans.Commands.CreateRentPlan
{
    public class CreateRentPlanCommand : IRequest<int>
    {
       
        public string Description { get; set; } = default!;

       
        public int Days { get; set; }

        
        public decimal Cost { get; set; }
    }
}
