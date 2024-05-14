using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Repositories;

namespace MotorcycleRental.Application.RentPlans.Commands.CreateRentPlan
{
    public class CreateRentPlanCommandHandler(
        IRentPlansRepository repository,
        ILogger<CreateRentPlanCommandHandler> logger,
        IMapper mapper
        ) : IRequestHandler<CreateRentPlanCommand, int>
    {
        public async Task<int> Handle(CreateRentPlanCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Creating a new Rental Plan: {request}");

            //do mapping
            var rentalPlan = mapper.Map<RentPlan>(request);

            //insert
            return await repository.Create(rentalPlan);             

        }
    }
}
