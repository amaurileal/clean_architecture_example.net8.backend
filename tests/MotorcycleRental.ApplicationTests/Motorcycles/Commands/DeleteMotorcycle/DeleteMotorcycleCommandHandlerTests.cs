using Xunit;
using MotorcycleRental.Application.Motorcycles.Commands.DeleteMotorcycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Infrastructure.Repositories;
using System.Security.AccessControl;

namespace MotorcycleRental.Application.Motorcycles.Commands.DeleteMotorcycle.Tests
{
    public class DeleteMotorcycleCommandHandlerTests
    {
        private readonly Mock<IMotorcyclesRepository> _repositoryMock;
        private readonly DeleteMotorcycleCommandHandler _handler;
        private readonly Mock<ILogger<DeleteMotorcycleCommandHandler>> _loggerMock;

        public DeleteMotorcycleCommandHandlerTests()
        {
            _repositoryMock = new Mock<IMotorcyclesRepository>();
            _loggerMock = new Mock<ILogger<DeleteMotorcycleCommandHandler>>();
            _handler = new DeleteMotorcycleCommandHandler(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ValidMotorcycleId_ShouldCallDelete()
        {
            // Arrange
            var motorcycleId = 123;
            var motorcycle = new Motorcycle { Id = motorcycleId };
            _repositoryMock.Setup(r => r.GetByIdAsync(motorcycleId))
                           .ReturnsAsync(motorcycle);

            var command = new DeleteMotorcycleCommand( motorcycleId );

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Delete(It.Is<Motorcycle>(m => m.Id == motorcycleId)), Times.Once);
            //_loggerMock.Verify(l => l.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidMotorcycleId_ShouldThrowNotFoundException()
        {
            // Arrange
            var invalidMotorcycleId = -1;
            _repositoryMock.Setup(r => r.GetByIdAsync(invalidMotorcycleId))
                           .ReturnsAsync((Motorcycle)null);

            var command = new DeleteMotorcycleCommand( invalidMotorcycleId );

            // Act & Assert
            var exception = await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));

            Xunit.Assert.Equal($"Motorcycle with id: {invalidMotorcycleId} doesn't exists", exception.Message);
            
        }
    }
}