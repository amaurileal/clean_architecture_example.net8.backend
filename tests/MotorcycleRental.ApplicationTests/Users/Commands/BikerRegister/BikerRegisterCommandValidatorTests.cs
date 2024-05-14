using FluentValidation.TestHelper;
using MotorcycleRental.Application.Bikers.Dtos;
using Xunit;

namespace MotorcycleRental.Application.Users.Commands.BikerRegister.Tests
{
    public class BikerRegisterCommandValidatorTests
    {
        private readonly BikerRegisterCommandValidator _validator = new BikerRegisterCommandValidator();

        [Fact]
        public void Email_WhenInvalid_ShouldHaveValidationError()
        {
            // Arrange
            var command = new BikerRegisterCommand { Email = "notanemail" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(cmd => cmd.Email);
        }

        [Fact]
        public void Email_WhenValid_ShouldNotHaveValidationError()
        {
            // Arrange
            var command = new BikerRegisterCommand { Email = "valid@example.com" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(cmd => cmd.Email);
        }

        [Fact]
        public void CNH_WhenInvalid_ShouldHaveValidationError()
        {
            // Arrange
            var command = new BikerRegisterCommand { CreateBikerDto = new CreateBikerDto { CNH = "123456" } };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(cmd => cmd.CreateBikerDto.CNH);
        }

        [Fact]
        public void CNH_WhenValid_ShouldNotHaveValidationError()
        {
            // Arrange
            var command = new BikerRegisterCommand { CreateBikerDto = new CreateBikerDto { CNH = "12345678901" } };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(cmd => cmd.CreateBikerDto.CNH);
        }

        [Fact]
        public void CNPJ_WhenInvalid_ShouldHaveValidationError()
        {
            // Arrange
            var command = new BikerRegisterCommand { CreateBikerDto = new CreateBikerDto { CNPJ = "12345678901234" } };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(cmd => cmd.CreateBikerDto.CNPJ);
        }

        [Fact]
        public void CNPJ_WhenValid_ShouldNotHaveValidationError()
        {
            // Arrange
            var command = new BikerRegisterCommand { CreateBikerDto = new CreateBikerDto { CNPJ = "71.141.109/0001-97" } };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(cmd => cmd.CreateBikerDto.CNPJ);
        }

        [Fact]
        public void CNHType_WhenInvalid_ShouldHaveValidationError()
        {
            // Arrange
            var command = new BikerRegisterCommand { CreateBikerDto = new CreateBikerDto { CNHType = "C" } };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(cmd => cmd.CreateBikerDto.CNHType);
        }

        [Fact]
        public void DateOfBirth_WhenTooYoung_ShouldHaveValidationError()
        {
            // Arrange
            var command = new BikerRegisterCommand { CreateBikerDto = new CreateBikerDto { DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-17)) } };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(cmd => cmd.CreateBikerDto.DateOfBirth);
        }
    }
}