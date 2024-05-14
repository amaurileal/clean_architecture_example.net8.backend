using FluentValidation.TestHelper;
using MotorcycleRental.Application.Motorcycles.Commands.CreateMotorcycle;
using Xunit;

namespace MotorcycleRental.ApplicationTests.Motorcycles.Commands.CreateMotorcycle
{
    public class CreateMotorcycleCommandValidatorTests
    {
        private readonly CreateMotorcycleCommandValidator validator;

        public CreateMotorcycleCommandValidatorTests()
        {
            validator = new CreateMotorcycleCommandValidator();
        }


        [Theory]
        [InlineData("A")]
        [InlineData("R")]
        [InlineData("S")]
        public void Status_WithValidValues_ShouldNotHaveValidationError(string status)
        {
            //Arrange
            var command = new CreateMotorcycleCommand { Status = status };
            //Act
            var result = validator.TestValidate(command);
            //Assert
            result.ShouldNotHaveValidationErrorFor(c => c.Status);
        }

        [Theory]
        [InlineData("X")]
        [InlineData("")]
        [InlineData(null)]
        public void Status_WithInvalidValues_ShouldHaveValidationError(string status)
        {
            //Arrange
            var command = new CreateMotorcycleCommand { Status = status };
            //Act
            var result = validator.TestValidate(command);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Status);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(1500)]
        [InlineData(9999)]
        public void Model_WithValidValues_ShouldNotHaveValidationError(int model)
        {
            //Arrange
            var command = new CreateMotorcycleCommand { Model = model };
            //Act
            var result = validator.TestValidate(command);
            //Assert
            result.ShouldNotHaveValidationErrorFor(c => c.Model);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(10000)]
        public void Model_WithInvalidValues_ShouldHaveValidationError(int model)
        {
            //Arrange
            var command = new CreateMotorcycleCommand { Model = model };
            //Act
            var result = validator.TestValidate(command);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Model);
        }

        [Theory]
        [InlineData("AAA-1234")]
        [InlineData("XYZ-1X23")]
        public void LicensePlate_WithValidValues_ShouldNotHaveValidationError(string licensePlate)
        {
            //Arrange
            var command = new CreateMotorcycleCommand { LicensePlate = licensePlate };
            //Act
            var result = validator.TestValidate(command);
            //Assert
            result.ShouldNotHaveValidationErrorFor(c => c.LicensePlate);
        }

        [Theory]
        [InlineData("AAA1234")]
        [InlineData("AA-1234")]
        [InlineData("1234-AAA")]
        public void LicensePlate_WithInvalidValues_ShouldHaveValidationError(string licensePlate)
        {
            //Arrange
            var command = new CreateMotorcycleCommand { LicensePlate = licensePlate };
            //Act
            var result = validator.TestValidate(command);
            //Assert
            result.ShouldHaveValidationErrorFor(c => c.LicensePlate);
        }
    }
}