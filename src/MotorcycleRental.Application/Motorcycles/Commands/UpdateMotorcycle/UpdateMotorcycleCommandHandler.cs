using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.Motorcycles.Commands.CreateMotorcycle;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Infrastructure.Repositories;

namespace MotorcycleRental.Application.Motorcycles.Commands.UpdateMotorcycle
{
    public class UpdateMotorcycleCommandHandler(
    IMotorcyclesRepository repository,
    ILogger<CreateMotorcycleCommandHandler> logger,
    IMapper mapper) : IRequestHandler<UpdateMotorcycleCommand>
    {
        public async Task Handle(UpdateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Motorcycle with id {MotorcycleId} with {@UpdateMotorcycle}",
               request.Id, request);

            var motorcycle = await repository.GetByIdAsync(request.Id);
            if (motorcycle is null)
                throw new NotFoundException(nameof(Motorcycle), request.Id.ToString());

            //check if the card is already registered
            var alreadyMotocycle = await repository.GetAllOrByLicensePlateAsync(request.LicensePlate,10,1, null, 0);
            if (alreadyMotocycle.Item2 > 0 && alreadyMotocycle.Item1.FirstOrDefault()!.Id != request.Id)
            {
                throw new BadRequestException($"There is already a motorcycle with this plate({request.LicensePlate}) registered");
            }

           

            mapper.Map(request, motorcycle);

            await repository.SaveChanges();
        }
    }
}
