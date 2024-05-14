using MediatR;
using MotorcycleRental.Application.Rents.Dtos;

namespace MotorcycleRental.Application.Rents.Queries.GetRentById
{
    public class GetRentByIdQuery(int id) : IRequest<RentDto>
    {
        public int Id { get; set; } = id;
    }
}
