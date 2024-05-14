using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.Common;
using MotorcycleRental.Application.Motorcycles.Dtos;
using MotorcycleRental.Application.Motorcycles.Queries.GetAllMotorcycles;
using MotorcycleRental.Infrastructure.Repositories;

namespace MotorcycleRental.Application.Motorcycles.Queries.GetAllMotorcycleActives
{
    public class GetAllActivesMotorcyclesQueryHandler(
         IMotorcyclesRepository repository,
        ILogger<GetAllMotorcyclesQueryHandler> logger,
        IMapper mapper
        ) : IRequestHandler<GetAllActivesMotorcyclesQuery, PagedResult<MotorcycleDto>>
    {
        public async Task<PagedResult<MotorcycleDto>> Handle(GetAllActivesMotorcyclesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all actives motorcycles");

            var (motorcycles, totalCount) = await repository.GetAllActivesMotorcyclesAsync(
                request.seach,
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
