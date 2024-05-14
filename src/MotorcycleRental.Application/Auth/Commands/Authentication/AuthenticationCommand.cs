using MediatR;
using MotorcycleRental.Application.Auth.Dtos;

namespace MotorcycleRental.Application.Auth.Commands.Authentication
{
    public class AuthenticationCommand : IRequest<LoginResultDto>
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
