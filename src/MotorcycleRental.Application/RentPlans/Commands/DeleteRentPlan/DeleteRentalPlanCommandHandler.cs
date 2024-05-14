using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;

namespace MotorcycleRental.Application.RentPlans.Commands.DeleteRentPlan
{
    public class DeleteRentalPlanCommandHandler(
        IRentPlansRepository repository,
        ILogger<DeleteRentalPlanCommandHandler> logger
        ) : IRequestHandler<DeleteRentalPlanCommand>
    {
        public async Task Handle(DeleteRentalPlanCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Removing Rental Plan with id: {RentalPlanId}",request.Id);

            var rentalPlan = await repository.GetByIdAsync(request.Id);

            if (rentalPlan is null)
                throw new NotFoundException(nameof(RentPlan), request.Id.ToString());

            await repository.Delete(rentalPlan);               

            
        }
    }
}
