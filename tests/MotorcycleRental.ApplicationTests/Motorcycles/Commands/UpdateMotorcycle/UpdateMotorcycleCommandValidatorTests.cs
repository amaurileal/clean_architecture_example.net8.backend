using FluentValidation.TestHelper;
using Xunit;

namespace MotorcycleRental.Application.Motorcycles.Commands.UpdateMotorcycle.Tests;

public class UpdateMotorcycleCommandValidatorTests
{
    private readonly UpdateMotorcycleCommandValidator _validator;

    public UpdateMotorcycleCommandValidatorTests()
    {
        _validator = new UpdateMotorcycleCommandValidator();
    }

    [Theory]
    [InlineData("ABC-1234")]
    [InlineData("XYZ-1A23")]
    public void LicensePlate_WithValidFormat_ShouldNotHaveValidationError(string licensePlate)
    {
        // Arrange
        var command = new UpdateMotorcycleCommand { LicensePlate = licensePlate };

        // Act & Assert
        // Validate the license plate and assert no validation errors for it
        _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.LicensePlate);
    }

    [Theory]
    [InlineData("ABC1234")]
    [InlineData("XYZ-12345")]
    [InlineData("XYZ-123")]
    [InlineData("1234-XYZA")]
    public void LicensePlate_WithInvalidFormat_ShouldHaveValidationError(string licensePlate)
    {
        // Arrange
        var command = new UpdateMotorcycleCommand { LicensePlate = licensePlate };

        // Act & Assert
        // Validate the license plate and assert it fails validation
        _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.LicensePlate);
    }
}