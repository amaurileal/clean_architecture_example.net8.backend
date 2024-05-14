using AutoMapper;
using MotorcycleRental.Application.Rents.Commands.CreateRent;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Application.Rents.Dtos
{
    public class RentsProfile : Profile
    {
        public RentsProfile()
        {
            CreateMap<CreateRentCommand, Rent>();
            CreateMap<Rent, RentDto>();           

        }
    }
}
