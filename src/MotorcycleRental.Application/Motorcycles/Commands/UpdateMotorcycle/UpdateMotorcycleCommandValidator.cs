using FluentValidation;

namespace MotorcycleRental.Application.Motorcycles.Commands.UpdateMotorcycle
{
    public class UpdateMotorcycleCommandValidator : AbstractValidator<UpdateMotorcycleCommand>
    {
        public UpdateMotorcycleCommandValidator()
        {
            RuleFor(dto => dto.LicensePlate).Matches(@"^([A-Z]{3}-[0-9]{4}|[A-Z]{3}-[0-9][A-Z][0-9]{2})$")
                    .WithMessage("Please provide a valid License Plate (###-####).");
        }
    }
}
