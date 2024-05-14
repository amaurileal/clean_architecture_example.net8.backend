using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.Motorcycles.Dtos;
using MotorcycleRental.Domain.Constants;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Infrastructure.Repositories;
using Xunit;

namespace MotorcycleRental.Application.Motorcycles.Queries.GetAllMotorcycles.Tests
{
    public class GetAllMotorcyclesQueryHandlerTests
    {
        private readonly Mock<IMotorcyclesRepository> _mockRepository = new Mock<IMotorcyclesRepository>();
        private readonly Mock<ILogger<GetAllMotorcyclesQueryHandler>> _mockLogger = new Mock<ILogger<GetAllMotorcyclesQueryHandler>>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly GetAllMotorcyclesQueryHandler _handler;

        public GetAllMotorcyclesQueryHandlerTests()
        {
            _handler = new GetAllMotorcyclesQueryHandler(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ReturnsCorrectData()
        {
            // Arrange
            var motorcycles = new List<Motorcycle> { new Motorcycle(), new Motorcycle() };
            var motorcycleDtos = new List<MotorcycleDto> { new MotorcycleDto(), new MotorcycleDto() };
            _mockRepository.Setup(repo => repo.GetAllOrByLicensePlateAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<SortDirection>()))
                .ReturnsAsync((motorcycles, motorcycles.Count));
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<MotorcycleDto>>(motorcycles)).Returns(motorcycleDtos);
            var query = new GetAllMotorcyclesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Xunit.Assert.Equal(motorcycleDtos.Count, result.Items.Count());
        }

        [Fact]
        public async Task Handle_ReturnsEmptyWhenNoData()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllOrByLicensePlateAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<SortDirection>()))
                .ReturnsAsync((new List<Motorcycle>(), 0));
            var query = new GetAllMotorcyclesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Xunit.Assert.Empty(result.Items);
        }

        [Fact]
        public async Task Handle_ThrowsExceptionWhenRepositoryFails()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllOrByLicensePlateAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<SortDirection>()))
                .ThrowsAsync(new System.Exception("Database error"));
            var query = new GetAllMotorcyclesQuery();

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<System.Exception>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}