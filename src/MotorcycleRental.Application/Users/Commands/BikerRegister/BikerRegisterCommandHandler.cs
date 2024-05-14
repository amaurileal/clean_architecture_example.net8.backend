using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;

namespace MotorcycleRental.Application.Users.Commands.BikerRegister
{
    public class BikerRegisterCommandHandler(
        IUsersRepository repository,
        IBikersRepository bikersRepository,
        ILogger<BikerRegisterCommandHandler> logger,
        IUserStore<User> userStore,
        IMapper mapper
        ) : IRequestHandler<BikerRegisterCommand, string>
    {

        public async Task<string> Handle(BikerRegisterCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a Bike User {BikeUser}", request);

            User user = new User();
            user.Email = request.Email;
            user.UserName = request.Email;
            user.NormalizedUserName = request.Email.ToUpper();
            user.NormalizedEmail = request.Email.ToUpper();

            var biker = mapper.Map<Biker>(request.CreateBikerDto);

            var duplicatedBiker = await  bikersRepository.GetBikerByCNPJOrCNH(biker);

            if (duplicatedBiker.Count() > 0)
                throw new BadRequestException($"Biker with CNPJ {biker.CNPJ} OR CNH {biker.CNH} already exists.");

            return await repository.InsertBiker(user, request.Password, biker);

        }
    }
}
