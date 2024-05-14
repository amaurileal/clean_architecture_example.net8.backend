using Amazon.S3.Model;
using FluentAssertions;
using Moq;
using MotorcycleRental.Application.Bikers.Commands.SendCHNImage;
using MotorcycleRental.ApplicationTests.Bikers.Commands.SendCNHImage.Commons;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using Xunit;

namespace MotorcycleRental.ApplicationTests.Bikers.Commands.SendCNHImage;

public class SendCNHImageCommandHandlerTests
{
    private readonly SendCNHImageCommandTestSetup _setup;

    public SendCNHImageCommandHandlerTests()
    {
        _setup = new SendCNHImageCommandTestSetup();
    }

    [Fact]
    public async Task Handle_ThrowsBadRequestException_IfBikerNotFound()
    {
        // Arrange
        _setup.BikersRepositoryMock.Setup(x => x.GetByUserIdAsync(_setup.TestUser.Id)).ReturnsAsync((Biker)null);
        var handler = _setup.CreateHandler();
        var command = new SendCNHImageCommand { CNHImage = _setup.CreateFormFile("Fake image content", "test.png", "image/png") };

        // Act & Assert
        await Xunit.Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, new CancellationToken()));
    }

    [Fact]
    public async Task Handle_ReturnsTrue_OnSuccessfulUpload()
    {
        // Arrange
        _setup.BikersRepositoryMock.Setup(x => x.GetByUserIdAsync(_setup.TestUser.Id)).ReturnsAsync(new Biker());
        _setup.S3ClientMock.Setup(x => x.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.FromResult(new PutObjectResponse()));

        var handler = _setup.CreateHandler();
        var command = new SendCNHImageCommand { CNHImage = _setup.CreateFormFile("Fake image content", "test.png", "image/png") };

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Xunit.Assert.True(result);
    }

    [Fact]
    public async Task Handle_ReturnsFalse_OnErrorForWrongFileType()
    {
        // Arrange
        _setup.BikersRepositoryMock.Setup(x => x.GetByUserIdAsync(_setup.TestUser.Id)).ReturnsAsync(new Biker());
        _setup.S3ClientMock.Setup(x => x.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.FromResult(new PutObjectResponse()));

        var handler = _setup.CreateHandler();
        var command = new SendCNHImageCommand { CNHImage = _setup.CreateFormFile("Fake image content", "test.jpg", "image/jpg") };

        // Act & Assert
        var exception = await Xunit.Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, new CancellationToken()));
        Xunit.Assert.Equal("Invalid file format. Only PNG and BMP files are allowed.", exception.Message);
        //Action action = async () => await handler.Handle(command, new CancellationToken());

        //// Assert
        //action.Should().Throw<BadRequestException>()
        //    .WithMessage("Invalid file format. Only PNG and BMP files are allowed.");
        ////Xunit.Assert.True(result);
    }
}