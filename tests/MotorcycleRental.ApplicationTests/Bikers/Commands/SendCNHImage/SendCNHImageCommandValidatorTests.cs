using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using MotorcycleRental.Application.Bikers.Commands.SendCHNImage;
using System.Text;
using Xunit;

namespace MotorcycleRental.ApplicationTests.Bikers.Commands.SendCNHImage
{
    public class SendCNHImageCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldHaveNotValidationErrors()
        {
            //arrange
            var validator = new SendCNHImageCommandValidator();
            var command = new SendCNHImageCommand();
            var content = "Fake image content";
            var fileName = "cnh.png";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var file = new FormFile(stream, 0, stream.Length, "CNHImage", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };
            command.CNHImage = file;


            //act

            var result = validator.TestValidate(command);

            //assert
            result.ShouldNotHaveAnyValidationErrors();

        }

        [Fact()]
        public void Validator_ForValidCommand_ShouldHaveValidationErrors()
        {
            // arrange
            var validator = new SendCNHImageCommandValidator();
            var command = new SendCNHImageCommand();

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldHaveValidationErrorFor(dto => dto.CNHImage);

        }
    }
}