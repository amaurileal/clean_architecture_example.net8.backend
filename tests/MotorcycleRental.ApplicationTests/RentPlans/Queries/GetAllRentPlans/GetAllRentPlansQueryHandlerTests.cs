using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.RentPlans.Dtos;
using MotorcycleRental.Application.RentPlans.Queries.GetAllRentPlans;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Repositories;
using Xunit;

namespace MotorcycleRental.ApplicationTests.RentPlans.Queries.GetAllRentPlans
{
    public class GetAllRentPlansQueryHandlerTests
    {
        private readonly Mock<IRentPlansRepository> _repositoryMock;
        private readonly Mock<ILogger<GetAllRentPlansQueryHandler>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetAllRentPlansQueryHandler _handler;

        public GetAllRentPlansQueryHandlerTests()
        {
            _repositoryMock = new Mock<IRentPlansRepository>();
            _loggerMock = new Mock<ILogger<GetAllRentPlansQueryHandler>>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetAllRentPlansQueryHandler(_repositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllRentalPlans()
        {
            // Arrange
            var rentPlans = new List<RentPlan>
        {
            new RentPlan {  Cost = 100, Description = "Basic Plan", Days = 1 },
            new RentPlan {  Cost = 200, Description = "Extended Plan", Days = 2 }
        };
            var rentPlanDtos = new List<RentPlanDto>
        {
            new RentPlanDto {  Cost = 100, Description = "Basic Plan", Days = 1 },
            new RentPlanDto {  Cost = 200, Description = "Extended Plan", Days = 2 }
        };

            _repositoryMock.Setup(r => r.GetAllByAsync()).ReturnsAsync(rentPlans);
            _mapperMock.Setup(m => m.Map<IEnumerable<RentPlanDto>>(rentPlans)).Returns(rentPlanDtos);

            // Act
            var result = await _handler.Handle(new GetAllRentPlansQuery(), CancellationToken.None);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(rentPlanDtos.Count, result.Count());
            _mapperMock.Verify(m => m.Map<IEnumerable<RentPlanDto>>(rentPlans), Times.Once);

        }
    }
}