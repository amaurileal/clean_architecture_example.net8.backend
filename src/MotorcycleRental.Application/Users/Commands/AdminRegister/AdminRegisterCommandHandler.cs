using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Repositories;

namespace MotorcycleRental.Application.Users.Commands.AdminRegister
{
    public class AdminRegisterCommandHandler(
        IUsersRepository respository,
        ILogger<AdminRegisterCommandHandler> logger
        ) : IRequestHandler<AdminRegisterCommand,string>
    {
        public async Task<string> Handle(AdminRegisterCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a Admin User {@request}", request);

            User user = new User();            
            user.Email = request.Email;
            user.UserName = request.Email;
            user.NormalizedUserName = request.Email.ToUpper();
            user.NormalizedEmail = request.Email.ToUpper();



            var identityResult = await respository.InsertAdmin(user, request.Password);

            return identityResult;
        }
    }
}
