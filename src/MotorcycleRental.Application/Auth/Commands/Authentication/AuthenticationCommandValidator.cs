using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.Json;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Application.Auth.Commands.Authentication
{
    public class AuthenticationCommandValidator : AbstractValidator<AuthenticationCommand>
    {

        public AuthenticationCommandValidator()
        {

            RuleFor(dto => dto.Email)
      .NotEmpty()
      .EmailAddress();

            RuleFor(dto => dto.Password)
                .NotEmpty();

        }

    }
}
