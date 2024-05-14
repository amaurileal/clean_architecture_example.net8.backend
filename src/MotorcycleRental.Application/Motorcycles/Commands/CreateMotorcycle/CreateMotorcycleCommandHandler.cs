using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.Interfaces;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Infrastructure.Repositories;

namespace MotorcycleRental.Application.Motorcycles.Commands.CreateMotorcycle;

public class CreateMotorcycleCommandHandler(
    IMotorcyclesRepository repository,
    ILogger<CreateMotorcycleCommandHandler> logger,
    IMapper mapper,
    IMessageQueueService messageQueueService) : IRequestHandler<CreateMotorcycleCommand, int>
{
    public async Task<int> Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new motorcycle");

        var motorcycle = mapper.Map<Motorcycle>(request);

        //check if the card is already registered
        var alreadyMotocycle = await repository.GetAllOrByLicensePlateAsync(request.LicensePlate, 1, 10, null, 0);
        if(alreadyMotocycle.Item2 > 0)
        {
            throw new BadRequestException($"There is already a motorcycle with this plate({request.LicensePlate}) registered");
        }

        int id = await repository.Create(motorcycle);

        //Generate Event to Message Broker RabbitMQ...
        messageQueueService.Publish(motorcycle);

        return id;
    }
}
