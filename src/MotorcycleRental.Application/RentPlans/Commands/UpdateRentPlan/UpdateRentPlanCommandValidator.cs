using FluentValidation;

namespace MotorcycleRental.Application.RentPlans.Commands.UpdateRentPlan
{
    public class UpdateRentPlanCommandValidator : AbstractValidator<UpdateRentPlanCommand>
    {
        public UpdateRentPlanCommandValidator()
        {
            RuleFor(dto => dto.Cost).NotEmpty();
            RuleFor(dto => dto.Description).NotEmpty();
            RuleFor(dto => dto.Days).NotEmpty();
        }
    }
}
