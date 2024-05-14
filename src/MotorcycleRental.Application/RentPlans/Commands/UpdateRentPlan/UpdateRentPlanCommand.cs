using MediatR;

namespace MotorcycleRental.Application.RentPlans.Commands.UpdateRentPlan
{
    public class UpdateRentPlanCommand : IRequest
    {
        public int Id { get; set; }


        public string Description { get; set; } = default!;


        public int Days { get; set; }


        public decimal Cost { get; set; }
    }
}
