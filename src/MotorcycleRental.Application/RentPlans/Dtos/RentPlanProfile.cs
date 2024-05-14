using AutoMapper;
using MotorcycleRental.Application.RentPlans.Commands.CreateRentPlan;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Application.RentPlans.Dtos
{
    public class RentPlanProfile : Profile
    {
        public RentPlanProfile()
        {
            CreateMap<CreateRentPlanCommand, RentPlan>();

            CreateMap<RentPlan, RentPlanDto>();

            CreateMap<RentPlanDto, RentPlan>();

        }
    }
}
