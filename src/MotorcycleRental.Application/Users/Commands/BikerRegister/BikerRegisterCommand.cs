using MediatR;
using Microsoft.AspNetCore.Identity;
using MotorcycleRental.Application.Bikers.Dtos;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Application.Users.Commands.BikerRegister
{
    public class BikerRegisterCommand : IRequest<string>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public CreateBikerDto CreateBikerDto { get; set; } = new();
    }
}
