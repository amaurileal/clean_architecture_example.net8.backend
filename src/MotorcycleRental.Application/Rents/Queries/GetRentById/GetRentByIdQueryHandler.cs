using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.Rents.Dtos;
using MotorcycleRental.Application.Users;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;

namespace MotorcycleRental.Application.Rents.Queries.GetRentById
{
    public class GetRentByIdQueryHandler(
        IRentsRepository rentsRepository,
        IBikersRepository bikersRepository,
        ILogger<GetRentByIdQueryHandler> logger,
        IMapper mapper,
        IUserContext userContext
        
        ) : IRequestHandler<GetRentByIdQuery, RentDto>
    {
        public async Task<RentDto> Handle(GetRentByIdQuery request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("Getting Rent By Id {}", request.Id);

            var biker = await bikersRepository.GetByUserIdAsync( currentUser.Id );
            if ( biker == null )
            {
                throw new NotFoundException(nameof(Biker), $"Biker for userid:{currentUser.Id}");
            }

            var rent = await rentsRepository.GetByIdAndByBikerIdAsync(request.Id, biker.Id);
            if(rent == null)
                throw new NotFoundException(nameof(Rent), $"Rent Not Found for user:{currentUser.Email} (Biker CNH:{biker.CNH})");

            var rentDto = mapper.Map<RentDto>( rent );

            return rentDto;
        }
    }
}
