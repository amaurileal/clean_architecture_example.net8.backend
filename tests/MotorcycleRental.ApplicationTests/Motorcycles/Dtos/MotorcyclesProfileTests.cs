using AutoMapper;
using MotorcycleRental.Application.Motorcycles.Commands.CreateMotorcycle;
using MotorcycleRental.Application.Motorcycles.Commands.UpdateMotorcycle;
using MotorcycleRental.Domain.Entities;
using Xunit;

namespace MotorcycleRental.Application.Motorcycles.Dtos.Tests
{
    public class AutoMapperConfigurationTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public AutoMapperConfigurationTests()
        {
            // Initialize AutoMapper with the MotorcyclesProfile
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MotorcyclesProfile());
            });

            _mapper = _configuration.CreateMapper();
        }

        

        [Fact]
        public void Should_Map_CreateMotorcycleCommand_To_Motorcycle()
        {
            // Arrange
            var command = new CreateMotorcycleCommand {Year=2021, Model = 2021, LicensePlate = "XYZ-1234", Status = "A" };

            // Act
            var motorcycle = _mapper.Map<Motorcycle>(command);

            // Assert
            Xunit.Assert.Equal(command.Model, motorcycle.Model);
            Xunit.Assert.Equal(command.LicensePlate, motorcycle.LicensePlate);
            Xunit.Assert.Equal(command.Status, motorcycle.Status);
            Xunit.Assert.Equal(command.Year, motorcycle.Year);

        }

        [Fact]
        public void Should_Map_UpdateMotorcycleCommand_To_Motorcycle()
        {
            // Arrange
            var command = new UpdateMotorcycleCommand { Id = 1, LicensePlate = "XYZ-5678"};

            // Act
            var motorcycle = _mapper.Map<Motorcycle>(command);

            // Assert
            Xunit.Assert.Equal(command.Id, motorcycle.Id);
            Xunit.Assert.Equal(command.LicensePlate, motorcycle.LicensePlate);
        }

        [Fact]
        public void Should_Map_Motorcycle_To_MotorcycleDto()
        {
            // Arrange
            var motorcycle = new Motorcycle { Model = 2021, Year=2021, LicensePlate = "XYZ-0000", Status = "S" };

            // Act
            var dto = _mapper.Map<MotorcycleDto>(motorcycle);

            // Assert
            Xunit.Assert.Equal(motorcycle.Model, dto.Model);
            Xunit.Assert.Equal(motorcycle.LicensePlate, dto.LicensePlate);
            Xunit.Assert.Equal(motorcycle.Status, dto.Status);
            Xunit.Assert.Equal(motorcycle.Year, dto.Year);

        }
    }
}