using FluentValidation;

namespace MotorcycleRental.Application.Rents.Commands.CreateRent
{
    public class CreateRentCommandValidator : AbstractValidator<CreateRentCommand>
    {
        public CreateRentCommandValidator()
        {
            RuleFor(dto => dto.MotorcycleId)
                .NotEmpty()
                .GreaterThan(0);

           

            RuleFor(dto => dto.RentPlanId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
