using FluentValidation.TestHelper;
using MotorcycleRental.Application.Auth.Commands.Authentication;
using Xunit;

namespace MotorcycleRental.Application.Auth.Commands.RefreshAuthentication.Tests
{
    public class RefreshAuthenticationCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldHaveNotValidationErrors()
        {
            // arrange

            var command = new RefreshAuthenticationCommand("fdsafdsafdsafdsafdfdsafsda");
            

            var validator = new RefreshAuthenticationCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForValidCommand_ShouldHaveValidationErrorsWhenEmptyContructor()
        {
            // arrange

            var command = new RefreshAuthenticationCommand("");

            var validator = new RefreshAuthenticationCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.RefreshToken);            
        }

        [Fact()]
        public void Validator_ForValidCommand_ShouldHaveValidationErrorsWhenNullContructor()
        {
            // arrange

            var command = new RefreshAuthenticationCommand(null);

            var validator = new RefreshAuthenticationCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.RefreshToken);
        }



    }
}