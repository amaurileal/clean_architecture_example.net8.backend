using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.Interfaces;
using MotorcycleRental.Application.Motorcycles.Commands.CreateMotorcycle;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Infrastructure.Repositories;
using Xunit;

namespace MotorcycleRental.ApplicationTests.Motorcycles.Commands.CreateMotorcycle;


public class CreateMotorcycleCommandHandlerTests
{
    private readonly Mock<IMotorcyclesRepository> _mockRepository = new Mock<IMotorcyclesRepository>();
    private readonly Mock<ILogger<CreateMotorcycleCommandHandler>> _mockLogger = new Mock<ILogger<CreateMotorcycleCommandHandler>>();
    private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
    private readonly Mock<IMessageQueueService> _mockMessageQueueService = new Mock<IMessageQueueService>();
    private readonly CreateMotorcycleCommandHandler _handler;

    public CreateMotorcycleCommandHandlerTests()
    {
        _handler = new CreateMotorcycleCommandHandler(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object, _mockMessageQueueService.Object);
    }

    [Fact]
    public async Task Handle_SuccessfulCreation_ReturnsMotorcycleId()
    {
        // Arrange
        var command = new CreateMotorcycleCommand { LicensePlate = "ABC123" };
        var motorcycle = new Motorcycle();
        _mockMapper.Setup(m => m.Map<Motorcycle>(command)).Returns(motorcycle);
        _mockRepository.Setup(r => r.GetAllOrByLicensePlateAsync("ABC123", 1, 10, null, 0))
                       .ReturnsAsync((new List<Motorcycle>(), 0));
        _mockRepository.Setup(r => r.Create(motorcycle)).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Xunit.Assert.Equal(1, result);
        _mockMessageQueueService.Verify(mq => mq.Publish(motorcycle), Times.Once);
    }

    [Fact]
    public async Task Handle_DuplicateLicensePlate_ThrowsBadRequestException()
    {
        // Arrange
        var command = new CreateMotorcycleCommand { LicensePlate = "ABC123" };
        var motorcycle = new Motorcycle();
        _mockMapper.Setup(m => m.Map<Motorcycle>(command)).Returns(motorcycle);
        _mockRepository.Setup(r => r.GetAllOrByLicensePlateAsync("ABC123", 1, 10, null, 0))
                       .ReturnsAsync((new List<Motorcycle> { new Motorcycle() }, 1));

        // Act & Assert
        await Xunit.Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_SuccessfulCreation_PublishesEvent()
    {
        // Arrange
        var command = new CreateMotorcycleCommand { LicensePlate = "ABC123" };
        var motorcycle = new Motorcycle();
        _mockMapper.Setup(m => m.Map<Motorcycle>(command)).Returns(motorcycle);
        _mockRepository.Setup(r => r.GetAllOrByLicensePlateAsync("ABC123", 1, 10, null, 0))
                       .ReturnsAsync((new List<Motorcycle>(), 0));
        _mockRepository.Setup(r => r.Create(motorcycle)).ReturnsAsync(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockMessageQueueService.Verify(mq => mq.Publish(motorcycle), Times.Once);
    }
}

