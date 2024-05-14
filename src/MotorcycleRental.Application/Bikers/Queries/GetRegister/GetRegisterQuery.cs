using MediatR;
using MotorcycleRental.Application.Bikers.Dtos;

namespace MotorcycleRental.Application.Bikers.Queries.GetRegister
{
    public class GetRegisterQuery : IRequest<BikerDto>
    {
    }
}
