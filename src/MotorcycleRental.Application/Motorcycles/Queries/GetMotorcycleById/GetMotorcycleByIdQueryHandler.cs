using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.Motorcycles.Dtos;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Infrastructure.Repositories;

namespace MotorcycleRental.Application.Motorcycles.Queries.GetMotorcycleById
{
    public class GetMotorcycleByIdQueryHandler(
        IMotorcyclesRepository repository,
    ILogger<GetMotorcycleByIdQueryHandler> logger,
    IMapper mapper
        ) : IRequestHandler<GetMotorcycleByIdQuery, MotorcycleDto>
    {
        public async Task<MotorcycleDto> Handle(GetMotorcycleByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Getting motorcycle by id: {request.Id}");
            var motorcycle = await repository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Motorcycle),request.Id.ToString());

            var motorcycleDto = mapper.Map<MotorcycleDto>(motorcycle);

            return motorcycleDto;
        }
    }
}
