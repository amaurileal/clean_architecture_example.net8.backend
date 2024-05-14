using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.RentPlans.Dtos;
using MotorcycleRental.Application.RentPlans.Queries.GetRentPlanById;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Repositories;
using Xunit;

namespace MotorcycleRent.Application.RentPlans.Queries.GetRentPlanById.Tests
{
    public class GetRentPlanByIdQueryHandlerTests
    {
        private readonly Mock<IRentPlansRepository> _repositoryMock;
        private readonly Mock<ILogger<GetRentPlanByIdQueryHandler>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetRentPlanByIdQueryHandler _handler;

        public GetRentPlanByIdQueryHandlerTests()
        {
            _repositoryMock = new Mock<IRentPlansRepository>();
            _loggerMock = new Mock<ILogger<GetRentPlanByIdQueryHandler>>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetRentPlanByIdQueryHandler(_repositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WhenRentPlanExists_ShouldReturnMappedDto()
        {
            // Arrange
            var rentPlanId = 123;
            var rentPlan = new RentPlan { Id = rentPlanId, Cost = 100, Description = "Basic Plan", Days = 1 };
            var rentPlanDto = new RentPlanDto {  Cost = 100, Description = "Basic Plan", Days = 1 };

            _repositoryMock.Setup(r => r.GetByIdAsync(rentPlanId)).ReturnsAsync(rentPlan);
            _mapperMock.Setup(m => m.Map<RentPlanDto>(rentPlan)).Returns(rentPlanDto);

            var query = new GetRentPlanByIdQuery(rentPlanId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Xunit.Assert.Equal(rentPlanDto, result);
            
        }

        [Fact]
        public async Task Handle_WhenRentPlanDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var nonExistingId = -1;
            _repositoryMock.Setup(r => r.GetByIdAsync(nonExistingId)).ReturnsAsync((RentPlan)null);

            var query = new GetRentPlanByIdQuery(nonExistingId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Xunit.Assert.Null(result);
            
        }
    }
}