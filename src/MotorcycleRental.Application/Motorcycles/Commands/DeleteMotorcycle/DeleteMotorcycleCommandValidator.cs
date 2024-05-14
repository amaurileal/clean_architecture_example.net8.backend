using FluentValidation;

namespace MotorcycleRental.Application.Motorcycles.Commands.DeleteMotorcycle
{
    public class DeleteMotorcycleCommandValidator : AbstractValidator<DeleteMotorcycleCommand>
    {
        public DeleteMotorcycleCommandValidator()
        {
            RuleFor(dto => dto.Id).NotEmpty();
        }
    }
}
