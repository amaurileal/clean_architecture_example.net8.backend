using MediatR;
using MotorcycleRental.Application.Motorcycles.Dtos;

namespace MotorcycleRental.Application.Motorcycles.Queries.GetMotorcycleById
{
    public class GetMotorcycleByIdQuery : IRequest<MotorcycleDto>
    {
        public int Id { get; set; }
    }
}
