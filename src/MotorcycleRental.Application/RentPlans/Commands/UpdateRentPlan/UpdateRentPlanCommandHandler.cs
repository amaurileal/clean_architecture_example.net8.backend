using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;

namespace MotorcycleRental.Application.RentPlans.Commands.UpdateRentPlan
{
    public class UpdateRentPlanCommandHandler(
         IRentPlansRepository repository,
        ILogger<UpdateRentPlanCommandHandler> logger,
        IMapper mapper
        ) : IRequestHandler<UpdateRentPlanCommand>
    {
        public async Task Handle(UpdateRentPlanCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating RentalPlan with id {RentalPlanId} with {@UpdateRentalPlan}",
                request.Id,request);

            var rentalPlan = await repository.GetByIdAsync(request.Id);
            if (rentalPlan is null)
                throw new NotFoundException(nameof(RentPlan), request.Id.ToString());

            //if (!rentalPlanAuthorizationService.Authorize(rentalPlan, ResourceOperation.Update))
            //    throw new ForbidException();

            mapper.Map(request, rentalPlan);            

            await repository.SaveChanges();
        }
    }
}
