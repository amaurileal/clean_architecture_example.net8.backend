using MediatR;
using MotorcycleRental.Application.Common;
using MotorcycleRental.Application.Motorcycles.Dtos;
using MotorcycleRental.Domain.Constants;

namespace MotorcycleRental.Application.Motorcycles.Queries.GetAllMotorcycles
{
    public class GetAllMotorcyclesQuery : IRequest<PagedResult<MotorcycleDto>>
    {
        public string? LicensePlate { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
