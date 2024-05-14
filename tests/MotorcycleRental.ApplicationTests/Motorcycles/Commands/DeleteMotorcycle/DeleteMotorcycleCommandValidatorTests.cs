using FluentValidation.TestHelper;
using Xunit;

namespace MotorcycleRental.Application.Motorcycles.Commands.DeleteMotorcycle.Tests
{
    public class DeleteMotorcycleCommandValidatorTests
    {
        private readonly DeleteMotorcycleCommandValidator validator;

        public DeleteMotorcycleCommandValidatorTests()
        {
            validator = new DeleteMotorcycleCommandValidator();
        }

        [Fact]
        public void Given_ValidId_Should_NotHaveValidationError()
        {
            // Arrange
            var command = new DeleteMotorcycleCommand(123); // Assume "123" is a valid non-empty ID

            // Act & Assert
            // Using FluentValidation's TestHelper to check for validation errors
            validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public void Given_InvalidOrEmptyId_Should_HaveValidationError(int invalidId)
        {
            // Arrange
            var command = new DeleteMotorcycleCommand(invalidId);

            // Act & Assert
            // Using FluentValidation's TestHelper to check for validation errors
            validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Id);
        }
    }
}