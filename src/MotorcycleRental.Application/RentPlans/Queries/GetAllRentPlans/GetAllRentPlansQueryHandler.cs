using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.RentPlans.Dtos;
using MotorcycleRental.Domain.Repositories;

namespace MotorcycleRental.Application.RentPlans.Queries.GetAllRentPlans
{
    public class GetAllRentPlansQueryHandler(
        IRentPlansRepository repository,
        ILogger<GetAllRentPlansQueryHandler> logger,
        IMapper mapper
        ) : IRequestHandler<GetAllRentPlansQuery, IEnumerable<RentPlanDto>>
    {
        public async Task<IEnumerable<RentPlanDto>> Handle(GetAllRentPlansQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting All Rental Plans");

            var rentalPlans = await repository.GetAllByAsync();

            var rentalPlanDtos = mapper.Map<IEnumerable<RentPlanDto>>(rentalPlans);

            return rentalPlanDtos;

        }
    }
}
