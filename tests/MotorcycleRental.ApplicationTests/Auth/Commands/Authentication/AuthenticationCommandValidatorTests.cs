using Xunit;
using MotorcycleRental.Application.Auth.Commands.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace MotorcycleRental.Application.Auth.Commands.Authentication.Tests
{
    public class AuthenticationCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldHaveNotValidationErrors()
        {
            // arrange

            var command = new AuthenticationCommand()
            {
                Email = "teste@test.com",
                Password = "TestPa!12"
            };

            var validator = new AuthenticationCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForValidCommand_ShouldHaveValidationErrors()
        {
            // arrange

            var command = new AuthenticationCommand()
            {
                Email = "testet.com"
            };

            var validator = new AuthenticationCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c  => c.Email);
            result.ShouldHaveValidationErrorFor(c => c.Password);
        }
    }
}