using AutoMapper;
using MotorcycleRental.Application.RentPlans.Commands.CreateRentPlan;
using MotorcycleRental.Domain.Entities;
using Xunit;

namespace MotorcycleRental.Application.RentPlans.Dtos.Tests
{
    public class AutoMapperConfigurationTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public AutoMapperConfigurationTests()
        {
            // Initialize AutoMapper with the RentPlanProfile
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RentPlanProfile());
            });

            _mapper = _configuration.CreateMapper();
        }

        

        [Fact]
        public void Should_Map_CreateRentPlanCommand_To_RentPlan()
        {
            // Arrange
            var command = new CreateRentPlanCommand
            {
                Cost = 299,
                Description = "Unlimited mileage for a weekend",
                Days = 3
            };

            // Act
            var rentPlan = _mapper.Map<RentPlan>(command);

            // Assert
            Xunit.Assert.Equal(command.Cost, rentPlan.Cost);
            Xunit.Assert.Equal(command.Description, rentPlan.Description);
            Xunit.Assert.Equal(command.Days, rentPlan.Days);
        }

        [Fact]
        public void Should_Map_RentPlan_To_RentPlanDto()
        {
            // Arrange
            var rentPlan = new RentPlan
            {
                
                Cost = 499,
                Description = "A full week rental with limited mileage",
                Days = 7,
            };

            // Act
            var dto = _mapper.Map<RentPlanDto>(rentPlan);

            // Assert
            Xunit.Assert.Equal(rentPlan.Cost, dto.Cost);
            Xunit.Assert.Equal(rentPlan.Description, dto.Description);
            Xunit.Assert.Equal(rentPlan.Days, dto.Days);
        }
    }
}