using FluentValidation.TestHelper;
using Xunit;

namespace MotorcycleRental.Application.RentPlans.Commands.CreateRentPlan.Tests
{
    public class CreateRentPlanCommandValidatorTests
    {
        private readonly CreateRentPlanCommandValidator _validator;

        public CreateRentPlanCommandValidatorTests()
        {
            _validator = new CreateRentPlanCommandValidator();
        }

        [Fact]
        public void Cost_WhenEmpty_ShouldHaveValidationError()
        {
            // Arrange
            var command = new CreateRentPlanCommand { Cost = 0, Description = "Test Plan", Days = 1 };

            // Act & Assert
            _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Cost);
        }

        [Fact]
        public void Description_WhenEmpty_ShouldHaveValidationError()
        {
            // Arrange
            var command = new CreateRentPlanCommand { Cost = 100, Description = "", Days = 1 };

            // Act & Assert
            _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Description);
        }

        [Fact]
        public void Days_WhenEmpty_ShouldHaveValidationError()
        {
            // Arrange
            var command = new CreateRentPlanCommand { Cost = 100, Description = "Test Plan", Days = 0 };

            // Act & Assert
            _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Days);
        }

        [Theory]
        [InlineData(100, "Valid Description", 1)]
        public void Command_WithValidFields_ShouldNotHaveValidationError(decimal cost, string description, int days)
        {
            // Arrange
            var command = new CreateRentPlanCommand { Cost = cost, Description = description, Days = days };

            // Act & Assert
            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Cost);
            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Description);
            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Days);
        }
    }
}