using MediatR;
using MotorcycleRental.Application.RentPlans.Dtos;

namespace MotorcycleRental.Application.RentPlans.Queries.GetRentPlanById
{
    public class GetRentPlanByIdQuery(int id) : IRequest<RentPlanDto>
    {
        public int Id { get; set; } = id;
    }
}
