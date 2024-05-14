using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;
using Xunit;

namespace MotorcycleRental.Application.RentPlans.Commands.UpdateRentPlan.Tests
{
    public class UpdateRentPlanCommandHandlerTests
    {
        private readonly Mock<IRentPlansRepository> _repositoryMock;
        private readonly Mock<ILogger<UpdateRentPlanCommandHandler>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateRentPlanCommandHandler _handler;

        public UpdateRentPlanCommandHandlerTests()
        {
            _repositoryMock = new Mock<IRentPlansRepository>();
            _loggerMock = new Mock<ILogger<UpdateRentPlanCommandHandler>>();
            _mapperMock = new Mock<IMapper>();
            _handler = new UpdateRentPlanCommandHandler(_repositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WhenRentPlanExists_ShouldUpdateRentPlan()
        {
            // Arrange
            var rentPlanId = 123;
            var rentPlan = new RentPlan();
            var command = new UpdateRentPlanCommand { Id = rentPlanId };

            _repositoryMock.Setup(r => r.GetByIdAsync(rentPlanId)).ReturnsAsync(rentPlan);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mapperMock.Verify(m => m.Map<UpdateRentPlanCommand, RentPlan>(command, rentPlan), Times.Once);
            _repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
            
        }

        [Fact]
        public async Task Handle_WhenRentPlanDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var nonExistingId = -1;
            var command = new UpdateRentPlanCommand { Id = nonExistingId };

            _repositoryMock.Setup(r => r.GetByIdAsync(nonExistingId)).ReturnsAsync((RentPlan)null);

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}