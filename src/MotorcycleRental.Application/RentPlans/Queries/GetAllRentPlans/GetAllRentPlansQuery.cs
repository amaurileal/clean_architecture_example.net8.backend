using MediatR;
using MotorcycleRental.Application.RentPlans.Dtos;

namespace MotorcycleRental.Application.RentPlans.Queries.GetAllRentPlans
{
    public class GetAllRentPlansQuery : IRequest<IEnumerable<RentPlanDto>>
    {
    }
}
