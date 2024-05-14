using FluentValidation.TestHelper;
using Xunit;

namespace MotorcycleRental.Application.Rents.Commands.CreateRent.Tests
{
    public class CreateRentCommandValidatorTests
    {
        private readonly CreateRentCommandValidator _validator = new CreateRentCommandValidator();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void MotorcycleId_Invalid(int invalidMotorcycleId)
        {
            // Arrange
            var command = new CreateRentCommand { MotorcycleId = invalidMotorcycleId, RentPlanId = 1 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.MotorcycleId);
        }

        [Fact]
        public void MotorcycleId_Valid()
        {
            // Arrange
            var command = new CreateRentCommand { MotorcycleId = 1, RentPlanId = 1 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.MotorcycleId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void RentPlanId_Invalid(int invalidRentPlanId)
        {
            // Arrange
            var command = new CreateRentCommand { MotorcycleId = 1, RentPlanId = invalidRentPlanId };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.RentPlanId);
        }

        [Fact]
        public void RentPlanId_Valid()
        {
            // Arrange
            var command = new CreateRentCommand { MotorcycleId = 1, RentPlanId = 1 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.RentPlanId);
        }
    }
}
