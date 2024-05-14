using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.RentPlans.Dtos;
using MotorcycleRental.Domain.Repositories;

namespace MotorcycleRental.Application.RentPlans.Queries.GetRentPlanById
{
    public class GetRentPlanByIdQueryHandler(
        IRentPlansRepository repository,
        ILogger<GetRentPlanByIdQueryHandler> logger,
        IMapper mapper
        ) : IRequestHandler<GetRentPlanByIdQuery, RentPlanDto>
    {
        public async Task<RentPlanDto> Handle(GetRentPlanByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting Rental Plan By Id: {RentalPlaId}", request.Id);

            var rentalPlan = await repository.GetByIdAsync(request.Id);

            return mapper.Map<RentPlanDto>(rentalPlan);
        }
    }
}
