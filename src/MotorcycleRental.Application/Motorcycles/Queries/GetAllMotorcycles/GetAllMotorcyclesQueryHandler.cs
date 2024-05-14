using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.Common;
using MotorcycleRental.Application.Motorcycles.Dtos;
using MotorcycleRental.Infrastructure.Repositories;

namespace MotorcycleRental.Application.Motorcycles.Queries.GetAllMotorcycles
{
    public class GetAllMotorcyclesQueryHandler(
        IMotorcyclesRepository repository,
        ILogger<GetAllMotorcyclesQueryHandler> logger,
        IMapper mapper
        ) : IRequestHandler<GetAllMotorcyclesQuery, PagedResult<MotorcycleDto>>
    {

        public async Task<PagedResult<MotorcycleDto>> Handle(GetAllMotorcyclesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all motorcycles");

            var (motorcycles, totalCount) = await repository.GetAllOrByLicensePlateAsync(
                request.LicensePlate,
                request.PageSize,
                request.PageNumber,
                request.SortBy,
            request.SortDirection);

            var motorcyclesDtos = mapper.Map<IEnumerable<MotorcycleDto>>(motorcycles);

            var result = new PagedResult<MotorcycleDto>(motorcyclesDtos, totalCount, request.PageSize, request.PageNumber);

            return result;
        }

    }
}
