using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.Bikers.Dtos;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;
using Xunit;

namespace MotorcycleRental.Application.Users.Commands.BikerRegister.Tests
{
    public class BikerRegisterCommandHandlerTests
    {
        private readonly Mock<IUsersRepository> _mockUsersRepository = new Mock<IUsersRepository>();
        private readonly Mock<IBikersRepository> _mockBikersRepository = new Mock<IBikersRepository>();
        private readonly Mock<ILogger<BikerRegisterCommandHandler>> _mockLogger = new Mock<ILogger<BikerRegisterCommandHandler>>();
        private readonly Mock<IUserStore<User>> _mockUserStore = new Mock<IUserStore<User>>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly BikerRegisterCommandHandler _handler;

        public BikerRegisterCommandHandlerTests()
        {
            _handler = new BikerRegisterCommandHandler(_mockUsersRepository.Object, _mockBikersRepository.Object, _mockLogger.Object, _mockUserStore.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_SuccessfulRegistration_ReturnsUserId()
        {
            // Arrange
            var command = new BikerRegisterCommand
            {
                Email = "test@example.com",
                Password = "SecurePassword123!",
                CreateBikerDto = new CreateBikerDto { CNPJ = "12345678901234", CNH = "D12345678" }
            };
            var biker = new Biker { CNPJ = command.CreateBikerDto.CNPJ, CNH = command.CreateBikerDto.CNH };
            _mockMapper.Setup(m => m.Map<Biker>(command.CreateBikerDto)).Returns(biker);
            _mockBikersRepository.Setup(repo => repo.GetBikerByCNPJOrCNH(biker)).ReturnsAsync(new List<Biker>());
            _mockUsersRepository.Setup(repo => repo.InsertBiker(It.IsAny<User>(), command.Password, biker)).ReturnsAsync("UserId");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Xunit.Assert.Equal("UserId", result);
        }

        [Fact]
        public async Task Handle_DuplicateBiker_ThrowsBadRequestException()
        {
            // Arrange
            var command = new BikerRegisterCommand
            {
                Email = "test@example.com",
                Password = "SecurePassword123!",
                CreateBikerDto = new CreateBikerDto { CNPJ = "12345678901234", CNH = "D12345678" }
            };
            var biker = new Biker { CNPJ = command.CreateBikerDto.CNPJ, CNH = command.CreateBikerDto.CNH };
            _mockMapper.Setup(m => m.Map<Biker>(command.CreateBikerDto)).Returns(biker);
            _mockBikersRepository.Setup(repo => repo.GetBikerByCNPJOrCNH(biker)).ReturnsAsync(new List<Biker> { new Biker() }); // Simulating existing biker

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}