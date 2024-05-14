using FluentValidation;

namespace MotorcycleRental.Application.RentPlans.Commands.CreateRentPlan
{
    public class CreateRentPlanCommandValidator : AbstractValidator<CreateRentPlanCommand>
    {
        public CreateRentPlanCommandValidator()
        {
            RuleFor(dto => dto.Cost).NotEmpty();
            RuleFor(dto => dto.Description).NotEmpty();
            RuleFor(dto => dto.Days).NotEmpty();
        }
    }
}
