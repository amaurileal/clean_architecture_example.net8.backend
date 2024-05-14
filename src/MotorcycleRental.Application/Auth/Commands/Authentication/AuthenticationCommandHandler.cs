using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.Auth.Dtos;
using MotorcycleRental.Application.Auth.Services;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;

namespace MotorcycleRental.Application.Auth.Commands.Authentication;

public class AuthenticationCommandHandler(
    ILogger<AuthenticationCommandHandler> logger,
    UserManager<User> userManager,
    ITokenService tokenService
    ) : IRequestHandler<AuthenticationCommand, LoginResultDto>
{
    public async Task<LoginResultDto> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Trying to do login...");
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user != null && await userManager.CheckPasswordAsync(user, request.Password))
        {

            var token = await tokenService.GenerateToken(user);
            var refreshToken = tokenService.GenerateRefreshToken(user);

            return new LoginResultDto()
            {
                TokenType = "Bearer",
                AccessToken = token,
                ExpiresIn = 3600,
                RefreshToken = refreshToken
            };
        }

        throw new BadRequestException("Email or password invalid");


    }
}
