using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;
using Xunit;

namespace MotorcycleRental.Application.RentPlans.Commands.DeleteRentPlan.Tests
{
    public class DeleteRentalPlanCommandHandlerTests
    {
        private readonly Mock<IRentPlansRepository> _repositoryMock;
        private readonly Mock<ILogger<DeleteRentalPlanCommandHandler>> _loggerMock;
        private readonly DeleteRentalPlanCommandHandler _handler;

        public DeleteRentalPlanCommandHandlerTests()
        {
            _repositoryMock = new Mock<IRentPlansRepository>();
            _loggerMock = new Mock<ILogger<DeleteRentalPlanCommandHandler>>();
            _handler = new DeleteRentalPlanCommandHandler(_repositoryMock.Object, _loggerMock.Object); // Mapper is not used in this scenario
        }

        [Fact]
        public async Task Handle_WhenRentalPlanExists_ShouldCallDelete()
        {
            // Arrange
            var rentalPlanId = 123;
            var rentalPlan = new RentPlan();
            _repositoryMock.Setup(r => r.GetByIdAsync(rentalPlanId)).ReturnsAsync(rentalPlan);

            var command = new DeleteRentalPlanCommand(rentalPlanId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Delete(rentalPlan), Times.Once);
            //_loggerMock.Verify(l => l.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenRentalPlanDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var invalidRentalPlanId = -1;
            _repositoryMock.Setup(r => r.GetByIdAsync(invalidRentalPlanId)).ReturnsAsync((RentPlan)null);

            var command = new DeleteRentalPlanCommand(invalidRentalPlanId);

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}