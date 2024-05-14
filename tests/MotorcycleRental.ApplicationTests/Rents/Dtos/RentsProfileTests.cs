using AutoMapper;
using MotorcycleRental.Application.Rents.Commands.CreateRent;
using MotorcycleRental.Domain.Entities;
using Xunit;

namespace MotorcycleRental.Application.Rents.Dtos.Tests
{
    public class RentsProfileTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public RentsProfileTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RentsProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        //[Fact]
        //public void AutoMapper_Configuration_IsValid()
        //{
        //    _configuration.AssertConfigurationIsValid();
        //}

        [Fact]
        public void CreateRentCommand_To_Rent_Mapping_IsValid()
        {
            // Arrange
            var command = new CreateRentCommand
            {
                MotorcycleId = 1,
                RentPlanId = 2,
                // Add other properties as needed for the test
            };

            // Act
            var rent = _mapper.Map<Rent>(command);

            // Assert
            Xunit.Assert.NotNull(rent);
            Xunit.Assert.Equal(command.MotorcycleId, rent.MotorcycleId);
            Xunit.Assert.Equal(command.RentPlanId, rent.RentPlanId);
            // Assert other properties as needed
        }

        [Fact]
        public void Rent_To_RentDto_Mapping_IsValid()
        {
            // Arrange
            var rent = new Rent
            {
                Id = 1,                
                MotorcycleId = 1,
                RentPlanId = 1,
                BikerId =1
            };

            // Act
            var rentDto = _mapper.Map<RentDto>(rent);

            // Assert
            Xunit.Assert.NotNull(rentDto);
            Xunit.Assert.Equal(rent.Motorcycle.Id, rentDto.Motorcycle.Id);
            Xunit.Assert.Equal(rent.RentPlan.Id, rentDto.RentPlan.Id);
            // Assert other properties as needed
        }
    }
}