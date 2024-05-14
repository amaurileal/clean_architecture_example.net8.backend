using MediatR;
using MotorcycleRental.Application.Auth.Dtos;

namespace MotorcycleRental.Application.Auth.Commands.RefreshAuthentication
{
    public class RefreshAuthenticationCommand(string refreshToken) : IRequest<LoginResultDto>
    {
        public string RefreshToken { get; set; } = refreshToken;
    }
}
