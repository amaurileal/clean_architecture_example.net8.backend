using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.Motorcycles.Dtos;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Infrastructure.Repositories;
using Xunit;

namespace MotorcycleRental.Application.Motorcycles.Queries.GetMotorcycleById.Tests
{
    public class GetMotorcycleByIdQueryHandlerTests
    {
        private readonly Mock<IMotorcyclesRepository> _mockRepository = new Mock<IMotorcyclesRepository>();
        private readonly Mock<ILogger<GetMotorcycleByIdQueryHandler>> _mockLogger = new Mock<ILogger<GetMotorcycleByIdQueryHandler>>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly GetMotorcycleByIdQueryHandler _handler;

        public GetMotorcycleByIdQueryHandlerTests()
        {
            _handler = new GetMotorcycleByIdQueryHandler(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ReturnsMotorcycleDto_WhenMotorcycleFound()
        {
            // Arrange
            var motorcycle = new Motorcycle
            {
                Id = 1,
                Year = 2020,
                Model = 2020,
                LicensePlate = "XYZ-1234",
                Status = "A"
            };
            var motorcycleDto = new MotorcycleDto
            {
                Id = 1,
                Model = 2020,
                LicensePlate = "XYZ-1234",
                Year = 2020
            };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(motorcycle);
            _mockMapper.Setup(mapper => mapper.Map<MotorcycleDto>(motorcycle)).Returns(motorcycleDto);
            var query = new GetMotorcycleByIdQuery { Id = 1 };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(motorcycleDto.Id, result.Id);
            Xunit.Assert.Equal(motorcycleDto.Model, result.Model);
        }

        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenMotorcycleNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(value: null);
            var query = new GetMotorcycleByIdQuery { Id = 1 };

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MapsEntityToDto_Correctly()
        {
            // Arrange
            var motorcycle = new Motorcycle
            {
                Id = 2,
                Year = 2018,
                Model = 2018,
                LicensePlate = "ABC-1234",
                Status = "A"
            };
            var motorcycleDto = new MotorcycleDto
            {
                Id = 2,
                Model = 2018,
                LicensePlate = "ABC-1234",
                Year = 2018
            };
            _mockRepository.Setup(repo => repo.GetByIdAsync(2)).ReturnsAsync(motorcycle);
            _mockMapper.Setup(mapper => mapper.Map<MotorcycleDto>(motorcycle)).Returns(motorcycleDto);
            var query = new GetMotorcycleByIdQuery { Id = 2 };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _mockMapper.Verify(mapper => mapper.Map<MotorcycleDto>(motorcycle), Times.Once);
            Xunit.Assert.Equal(motorcycleDto.Year, result.Year);
            Xunit.Assert.Equal(motorcycleDto.Status, result.Status);
        }
    }
}