using AutoMapper;
using MotorcycleRental.Application.Motorcycles.Commands.CreateMotorcycle;
using MotorcycleRental.Application.Motorcycles.Commands.UpdateMotorcycle;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Application.Motorcycles.Dtos;

public class MotorcyclesProfile : Profile
{
    public MotorcyclesProfile()
    {
        CreateMap<CreateMotorcycleCommand, Motorcycle>();
        CreateMap<UpdateMotorcycleCommand, Motorcycle>();
        CreateMap<Motorcycle, MotorcycleDto>();
    }
}
