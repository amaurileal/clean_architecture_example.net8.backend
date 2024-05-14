using AutoMapper;
using MotorcycleRental.Domain.Entities;
using Xunit;

namespace MotorcycleRental.Application.Bikers.Dtos.Tests
{
    public class BikerProfileTests
    {
        private IMapper _mapper;

        public BikerProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BikerProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Should_Map_BikerDto_To_Biker()
        {
            // Arrange
            var dto = new BikerDto { Id = 1, CNH = "99999999999", CNHType = "A", CNPJ = "99.999.999/0001-99", DateOfBirth = new DateOnly(1990, 1, 20) };

            // Act
            var entity = _mapper.Map<Biker>(dto);

            // Assert
            Xunit.Assert.NotNull(entity);
            Xunit.Assert.Equal(dto.Id, entity.Id);
            Xunit.Assert.Equal(dto.CNH, entity.CNH);
            Xunit.Assert.Equal(dto.CNHType, entity.CNHType);
            Xunit.Assert.Equal(dto.CNPJ, entity.CNPJ);
            Xunit.Assert.Equal(dto.DateOfBirth, entity.DateOfBirth);

        }

        [Fact]
        public void Should_Map_Biker_To_BikerDto()
        {
            var biker = new Biker { Id = 1, CNH = "99999999999", CNHType = "A", CNPJ = "99.999.999/0001-99", DateOfBirth = new DateOnly(1990, 1, 20), };

            var dto = _mapper.Map<BikerDto>(biker);

            Xunit.Assert.NotNull(dto);
            Xunit.Assert.Equal(biker.Id, dto.Id);
            Xunit.Assert.Equal(biker.CNH, dto.CNH);
            Xunit.Assert.Equal(biker.CNHType, dto.CNHType);
            Xunit.Assert.Equal(biker.CNPJ, dto.CNPJ);
            Xunit.Assert.Equal(biker.DateOfBirth, dto.DateOfBirth);
        }

        [Fact]
        public void Should_Map_CreateBikerDto_To_Biker()
        {
            var createDto = new CreateBikerDto {  CNH = "99999999999", CNHType = "A", CNPJ = "99.999.999/0001-99", DateOfBirth = new DateOnly(1990, 1, 20), };

            var biker = _mapper.Map<Biker>(createDto);

            Xunit.Assert.NotNull(biker);
            Xunit.Assert.Equal(createDto.CNH, biker.CNH);
            Xunit.Assert.Equal(createDto.CNHType, biker.CNHType);
            Xunit.Assert.Equal(createDto.CNPJ, biker.CNPJ);
            Xunit.Assert.Equal(createDto.DateOfBirth, biker.DateOfBirth);


            Xunit.Assert.Equal(0, biker.Id); 
        }


    }
}