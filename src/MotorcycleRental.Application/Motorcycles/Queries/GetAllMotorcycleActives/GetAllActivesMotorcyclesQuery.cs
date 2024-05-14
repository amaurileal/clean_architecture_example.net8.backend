using MediatR;
using MotorcycleRental.Application.Common;
using MotorcycleRental.Application.Motorcycles.Dtos;
using MotorcycleRental.Domain.Constants;

namespace MotorcycleRental.Application.Motorcycles.Queries.GetAllMotorcycleActives;

public class GetAllActivesMotorcyclesQuery : IRequest<PagedResult<MotorcycleDto>>
{
    public string? seach { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; } = default!;
    public SortDirection SortDirection { get; set; }
}