using MediatR;
using Microsoft.AspNetCore.Identity;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Application.Users.Commands.AdminRegister
{
    public class AdminRegisterCommand : IRequest<string>
    {
        public string Email { get; set; }

        public string Password { get; set; }


    }
}
