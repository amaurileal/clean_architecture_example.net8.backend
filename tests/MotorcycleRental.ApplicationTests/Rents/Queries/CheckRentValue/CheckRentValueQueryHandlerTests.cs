using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.Users;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;
using Xunit;

namespace MotorcycleRental.Application.Rents.Queries.CheckRentValue.Tests
{
    public class CheckRentValueQueryHandlerTests
    {
        private readonly Mock<IRentsRepository> _mockRentsRepository = new Mock<IRentsRepository>();
        private readonly Mock<IBikersRepository> _mockBikersRepository = new Mock<IBikersRepository>();
        private readonly Mock<ILogger<CheckRentValueQueryHandler>> _mockLogger = new Mock<ILogger<CheckRentValueQueryHandler>>();
        private readonly Mock<IUserContext> _mockUserContext = new Mock<IUserContext>();
        private readonly CheckRentValueQueryHandler _handler;

        public CheckRentValueQueryHandlerTests()
        {
            _handler = new CheckRentValueQueryHandler(_mockRentsRepository.Object, _mockBikersRepository.Object, _mockLogger.Object, _mockUserContext.Object);
        }

        [Fact]
        public async Task Handle_BikerNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@example.com", []);
            var query = new CheckRentValueQuery(DateOnly.FromDateTime(DateTime.Now));
            _mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);
            _mockBikersRepository.Setup(x => x.GetByUserIdAsync(currentUser.Id)).ReturnsAsync(value: null);

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ActiveRentNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@example.com", []);
            var biker = new Biker { Id = 1, UserId = currentUser.Id };
            var query = new CheckRentValueQuery(DateOnly.FromDateTime(DateTime.Now));
            _mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);
            _mockBikersRepository.Setup(x => x.GetByUserIdAsync(currentUser.Id)).ReturnsAsync(biker);
            _mockRentsRepository.Setup(x => x.GetActiveRentByBiker(biker.Id)).ReturnsAsync(value: null);

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }

        // Additional tests for successful value calculation and other scenarios

        // Method to mock user, biker, and rent setup for successful calculation tests
        private void SetupMockForSuccessfulCalculation(DateOnly previewDate, DateOnly initialDate, DateOnly expectedPreviewDate, int days, decimal cost, decimal expectedValue)
        {
            var currentUser = new CurrentUser("1", "test@example.com", []);
            var biker = new Biker { Id = 1, UserId = currentUser.Id };
            var rent = new Rent
            {
                Id = 1,
                BikerId = biker.Id,
                InitialDate = initialDate,
                PreviewDate = expectedPreviewDate,
                RentPlan = new RentPlan { Days = days, Cost = cost }
            };
            var query = new CheckRentValueQuery( previewDate );
            _mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);
            _mockBikersRepository.Setup(x => x.GetByUserIdAsync(currentUser.Id)).ReturnsAsync(biker);
            _mockRentsRepository.Setup(x => x.GetActiveRentByBiker(biker.Id)).ReturnsAsync(rent);
            var calculatedValue = _handler.Handle(query, CancellationToken.None).Result;
            Xunit.Assert.Equal(expectedValue, calculatedValue);
        }
    }
}