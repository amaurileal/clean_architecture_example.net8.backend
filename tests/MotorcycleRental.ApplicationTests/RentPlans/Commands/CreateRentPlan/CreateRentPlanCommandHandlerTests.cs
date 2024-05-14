using Xunit;
using MotorcycleRental.Application.RentPlans.Commands.CreateRentPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Repositories;

namespace MotorcycleRental.Application.RentPlans.Commands.CreateRentPlan.Tests
{
    public class CreateRentPlanCommandHandlerTests
    {
        private readonly Mock<IRentPlansRepository> _repositoryMock;
        private readonly Mock<ILogger<CreateRentPlanCommandHandler>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CreateRentPlanCommandHandler _handler;

        public CreateRentPlanCommandHandlerTests()
        {
            _repositoryMock = new Mock<IRentPlansRepository>();
            _loggerMock = new Mock<ILogger<CreateRentPlanCommandHandler>>();
            _mapperMock = new Mock<IMapper>();
            _handler = new CreateRentPlanCommandHandler(_repositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateRentPlanSuccessfully()
        {
            // Arrange
            var command = new CreateRentPlanCommand {Cost=30,Days=7,Description = "Description" };
            var rentPlan = new RentPlan();

            _mapperMock.Setup(m => m.Map<RentPlan>(It.IsAny<CreateRentPlanCommand>()))
                       .Returns(rentPlan);
            _repositoryMock.Setup(r => r.Create(It.IsAny<RentPlan>()))
                           .ReturnsAsync(1); // Assuming 1 is the ID returned after creating the rent plan

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mapperMock.Verify(m => m.Map<RentPlan>(command), Times.Once);
            _repositoryMock.Verify(r => r.Create(rentPlan), Times.Once);
            Xunit.Assert.Equal(1, result);
        }

       
    }
}