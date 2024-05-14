using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.Motorcycles.Commands.CreateMotorcycle;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Infrastructure.Repositories;
using Xunit;

namespace MotorcycleRental.Application.Motorcycles.Commands.UpdateMotorcycle.Tests
{
    public class UpdateMotorcycleCommandHandlerTests
    {
        private readonly Mock<IMotorcyclesRepository> _mockRepository = new Mock<IMotorcyclesRepository>();
        private readonly Mock<ILogger<CreateMotorcycleCommandHandler>> _mockLogger = new Mock<ILogger<CreateMotorcycleCommandHandler>>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly UpdateMotorcycleCommandHandler _handler;

        public UpdateMotorcycleCommandHandlerTests()
        {
            _handler = new UpdateMotorcycleCommandHandler(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_SuccessfulUpdate()
        {
            // Arrange
            var motorcycle = new Motorcycle { Id = 1, LicensePlate = "ABC123" };
            var command = new UpdateMotorcycleCommand { Id = 1, LicensePlate = "ABC123" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(motorcycle);
            _mockRepository.Setup(repo => repo.GetAllOrByLicensePlateAsync("ABC123", 10, 1, null, 0))
                           .ReturnsAsync((new List<Motorcycle> { motorcycle }, 1));
            _mockRepository.Setup(repo => repo.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockRepository.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task Handle_MotorcycleNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var command = new UpdateMotorcycleCommand { Id = 99, LicensePlate = "XYZ987" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((Motorcycle)null);

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_DuplicateLicensePlate_ThrowsBadRequestException()
        {
            // Arrange
            var motorcycle = new Motorcycle { Id = 1, LicensePlate = "ABC123" };
            var anotherMotorcycle = new Motorcycle { Id = 2, LicensePlate = "XYZ987" };
            var command = new UpdateMotorcycleCommand { Id = 1, LicensePlate = "XYZ987" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(motorcycle);
            _mockRepository.Setup(repo => repo.GetAllOrByLicensePlateAsync("XYZ987", 10, 1, null, 0))
                           .ReturnsAsync((new List<Motorcycle> { anotherMotorcycle }, 1));

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}