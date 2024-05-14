using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Repositories;
using Xunit;

namespace MotorcycleRental.Application.Users.Commands.AdminRegister.Tests
{
    public class AdminRegisterCommandHandlerTests
    {
        private readonly Mock<IUsersRepository> _mockUsersRepository = new Mock<IUsersRepository>();
        private readonly Mock<ILogger<AdminRegisterCommandHandler>> _mockLogger = new Mock<ILogger<AdminRegisterCommandHandler>>();
        private readonly AdminRegisterCommandHandler _handler;

        public AdminRegisterCommandHandlerTests()
        {
            _handler = new AdminRegisterCommandHandler(_mockUsersRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_Success_ReturnsIdentityResult()
        {
            // Arrange
            var command = new AdminRegisterCommand { Email = "admin@example.com", Password = "SecurePassword123!" };
            var expectedIdentityResult = "Success";
            _mockUsersRepository.Setup(repo => repo.InsertAdmin(It.IsAny<User>(), It.IsAny<string>()))
                                .ReturnsAsync(expectedIdentityResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Xunit.Assert.Equal(expectedIdentityResult, result);
            _mockUsersRepository.Verify(repo => repo.InsertAdmin(It.Is<User>(u => u.Email == command.Email), command.Password), Times.Once);
        }

        [Fact]
        public async Task Handle_Failure_ReturnsError()
        {
            // Arrange
            var command = new AdminRegisterCommand { Email = "admin@example.com", Password = "SecurePassword123!" };
            var expectedIdentityResult = "Error";
            _mockUsersRepository.Setup(repo => repo.InsertAdmin(It.IsAny<User>(), It.IsAny<string>()))
                                .ReturnsAsync(expectedIdentityResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Xunit.Assert.Equal(expectedIdentityResult, result);
            _mockUsersRepository.Verify(repo => repo.InsertAdmin(It.Is<User>(u => u.Email == command.Email), command.Password), Times.Once);
        }
    }
}