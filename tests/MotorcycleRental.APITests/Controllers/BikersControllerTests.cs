using Xunit;
using MotorcycleRental.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.Bikers.Commands.SendCHNImage;

namespace MotorcycleRental.API.Controllers.Tests
{
    public class BikersControllerTests
    {
        [Fact]
        public async Task UploadImage_ReturnsOk_WithValidImage()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();           
            var controller = new BikersController(mediatorMock.Object);

            var content = "Fake image content";
            var fileName = "cnh.jpg";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var file = new FormFile(stream, 0, stream.Length, "CNHImage", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var command = new SendCNHImageCommand { CNHImage = file };
             
            mediatorMock.Setup(m => m.Send(It.IsAny<SendCNHImageCommand>(), default))
                        .ReturnsAsync(true);

            // Act
            var result = await controller.UploadImage(command);

            // Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            Xunit.Assert.Equal(true, okResult.Value);
        }
    }
}