using MediatR;

namespace MotorcycleRental.Application.RentPlans.Commands.DeleteRentPlan
{
    public class DeleteRentalPlanCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
