using FluentValidation.TestHelper;
using Xunit;
namespace MotorcycleRental.Application.RentPlans.Commands.UpdateRentPlan.Tests;

public class UpdateRentPlanCommandValidatorTests
{
    private readonly UpdateRentPlanCommandValidator _validator;

    public UpdateRentPlanCommandValidatorTests()
    {
        _validator = new UpdateRentPlanCommandValidator();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Cost_WhenNotPositive_ShouldHaveValidationError(decimal invalidCost)
    {
        // Arrange
        var command = new UpdateRentPlanCommand {  Description = "Valid Description", Days = 1 };

        // Act & Assert
        _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Cost);
    }

    [Fact]
    public void Cost_WhenPositive_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new UpdateRentPlanCommand { Cost = 100, Description = "Valid Description", Days = 1 };

        // Act & Assert
        _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Cost);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Description_WhenNullOrEmpty_ShouldHaveValidationError(string invalidDescription)
    {
        // Arrange
        var command = new UpdateRentPlanCommand { Cost = 100, Description = invalidDescription, Days = 1 };

        // Act & Assert
        _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Description);
    }

    [Fact]
    public void Description_WhenNotNullOrEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new UpdateRentPlanCommand { Cost = 100, Description = "Valid Description", Days = 1 };

        // Act & Assert
        _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Description);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Days_WhenNotPositive_ShouldHaveValidationError(int invalidDays)
    {
        // Arrange
        var command = new UpdateRentPlanCommand { Cost = 100, Description = "Valid Description" };

        // Act & Assert
        _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Days);
    }

    [Fact]
    public void Days_WhenPositive_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new UpdateRentPlanCommand { Cost = 100, Description = "Valid Description", Days = 1 };

        // Act & Assert
        _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Days);
    }
}