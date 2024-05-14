using FluentValidation;

namespace MotorcycleRental.Application.Auth.Commands.RefreshAuthentication
{
    public class RefreshAuthenticationCommandValidator : AbstractValidator<RefreshAuthenticationCommand>
    {
        public RefreshAuthenticationCommandValidator()
        {
            RuleFor(dto => dto.RefreshToken).NotEmpty();
        }
    }
}
