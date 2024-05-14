using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.Rents.Dtos;
using MotorcycleRental.Application.Users;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;
using Xunit;

namespace MotorcycleRental.Application.Rents.Queries.GetRentById.Tests
{
    public class GetRentByIdQueryHandlerTests
    {
        private readonly Mock<IRentsRepository> _mockRentsRepository = new Mock<IRentsRepository>();
        private readonly Mock<IBikersRepository> _mockBikersRepository = new Mock<IBikersRepository>();
        private readonly Mock<ILogger<GetRentByIdQueryHandler>> _mockLogger = new Mock<ILogger<GetRentByIdQueryHandler>>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<IUserContext> _mockUserContext = new Mock<IUserContext>();
        private readonly GetRentByIdQueryHandler _handler;

        public GetRentByIdQueryHandlerTests()
        {
            _handler = new GetRentByIdQueryHandler(_mockRentsRepository.Object, _mockBikersRepository.Object, _mockLogger.Object, _mockMapper.Object, _mockUserContext.Object);
        }

        [Fact]
        public async Task Handle_RentFound_ReturnsMappedRentDto()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@example.com", []);
            var biker = new Biker { Id = 1, UserId = currentUser.Id };
            var rent = new Rent { Id = 1, BikerId = biker.Id };
            var rentDto = new RentDto { Id = 1 };
            var query = new GetRentByIdQuery(1);

            _mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);
            _mockBikersRepository.Setup(x => x.GetByUserIdAsync(currentUser.Id)).ReturnsAsync(biker);
            _mockRentsRepository.Setup(x => x.GetByIdAndByBikerIdAsync(query.Id, biker.Id)).ReturnsAsync(rent);
            _mockMapper.Setup(m => m.Map<RentDto>(rent)).Returns(rentDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(rentDto.Id, result.Id);
        }

        [Fact]
        public async Task Handle_BikerNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@example.com", []);
            var query = new GetRentByIdQuery(1);

            _mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);
            _mockBikersRepository.Setup(x => x.GetByUserIdAsync(currentUser.Id)).ReturnsAsync(value: null);

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_RentNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@example.com", []);
            var biker = new Biker { Id = 1, UserId = currentUser.Id };
            var query = new GetRentByIdQuery(1);

            _mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);
            _mockBikersRepository.Setup(x => x.GetByUserIdAsync(currentUser.Id)).ReturnsAsync(biker);
            _mockRentsRepository.Setup(x => x.GetByIdAndByBikerIdAsync(query.Id, biker.Id)).ReturnsAsync(value: null);

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}