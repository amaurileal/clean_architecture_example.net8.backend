using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.Users;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;
using MotorcycleRental.Infrastructure.Repositories;

namespace MotorcycleRental.Application.Rents.Commands.CreateRent
{
    public class CreateRentCommandHandler(
        IRentsRepository rentsRepository,
        IRentPlansRepository rentPlansRepository,
        IBikersRepository bikersRepository,
        IMotorcyclesRepository motorcyclesRepository,
        IUserContext userContext,
        ILogger<CreateRentCommandHandler> logger,
        IMapper mapper
        ) : IRequestHandler<CreateRentCommand, int>
    {
        public async Task<int> Handle(CreateRentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("{UserEmail} [{UserId}] is creating a new Rent {@Rent}",
            currentUser.Email,
            currentUser.Id,
            request);

            

            //verify if exists RentPlan
            var rentPlan = await rentPlansRepository.GetByIdAsync(request.RentPlanId);
            if (rentPlan is null)
                throw new NotFoundException(nameof(RentPlan), request.RentPlanId.ToString());

            //verify if exists Biker
            var biker = await bikersRepository.GetByUserIdAsync(currentUser.Id);
            if(biker is null)
                throw new NotFoundException(nameof(Biker), $"User id:{currentUser.Id} - User Email:{currentUser.Email}");

            //verify if exists MotorcycleId
            var motorCycle = await motorcyclesRepository.GetByIdAsync(request.MotorcycleId);
            if(motorCycle is null)
                throw new NotFoundException(nameof(Motorcycle), request.MotorcycleId.ToString());

            //verify if exist Active Rent to Biker
            var gotenRent = await rentsRepository.GetActiveRentByBiker(biker.Id);

            if (gotenRent != null)
                throw new BadRequestException($"There is already rent");

            var rent = mapper.Map<Rent>(request);

            rent.RentPlan = rentPlan;
            rent.Biker = biker;
            rent.Motorcycle = motorCycle;

            //add preview and initial date
            rent.InitialDate = DateOnly.FromDateTime(DateTime.Now).AddDays(1);
            rent.PreviewDate = rent.InitialDate.AddDays(rentPlan.Days + 1);
 
            var result = await rentsRepository.Create(rent);

            return result;

        }
    }
}
