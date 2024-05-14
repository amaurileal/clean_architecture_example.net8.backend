using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.Auth.Commands.Authentication;
using MotorcycleRental.Application.Auth.Dtos;
using MotorcycleRental.Application.Auth.Services;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;

namespace MotorcycleRental.Application.Auth.Commands.RefreshAuthentication
{
    public class RefreshAuthenticationCommandHandler(
        ILogger<AuthenticationCommandHandler> logger,
        UserManager<User> userManager,
        
    ITokenService tokenService
        ) : IRequestHandler<RefreshAuthenticationCommand, LoginResultDto>
    {
        public async Task<LoginResultDto> Handle(RefreshAuthenticationCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("login with a refreshtoken...");
            var result = tokenService.validateRefreshToken(request.RefreshToken);
            if (!result.IsValid)
            {
                throw new BadRequestException("invalid refresh token");
            }
            var user = await userManager.FindByEmailAsync(result.Claims["email"].ToString());            
            if (user == null)
            { throw new NotFoundException(nameof(User), result.Claims["email"].ToString()); }
            if (user.LockoutEnabled)
            { throw new ForbiddenException("User blocked!"); }

            string token = await tokenService.GenerateToken(user);
            string refreshToken = tokenService.GenerateRefreshToken(user);

            return new LoginResultDto()
            {
                TokenType = "Bearer",
                AccessToken = token,
                ExpiresIn = 3600,
                RefreshToken = refreshToken
            };
           
        }
    }
}
