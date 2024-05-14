using Xunit;
using MotorcycleRental.Application.Motorcycles.Queries.GetAllMotorcycleActives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace MotorcycleRental.Application.Motorcycles.Queries.GetAllMotorcycleActives.Tests
{
    public class GetAllActivesMotorcyclesQueryValidatorTests
    {
        private readonly GetAllActivesMotorcyclesQueryValidator _validator = new GetAllActivesMotorcyclesQueryValidator();

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public void PageNumber_Valid(int pageNumber)
        {
            // Arrange
            var model = new GetAllActivesMotorcyclesQuery { PageNumber = pageNumber };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(query => query.PageNumber);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void PageNumber_Invalid(int pageNumber)
        {
            // Arrange
            var model = new GetAllActivesMotorcyclesQuery { PageNumber = pageNumber };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(query => query.PageNumber);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(30)]
        public void PageSize_Valid(int pageSize)
        {
            // Arrange
            var model = new GetAllActivesMotorcyclesQuery { PageSize = pageSize };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(query => query.PageSize);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(20)]
        [InlineData(25)]
        public void PageSize_Invalid(int pageSize)
        {
            // Arrange
            var model = new GetAllActivesMotorcyclesQuery { PageSize = pageSize };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(query => query.PageSize);
        }

        [Theory]
        [InlineData("Model")]
        [InlineData("LicensePlate")]
        [InlineData("Year")]
        [InlineData("Id")]
        public void SortBy_Valid(string sortBy)
        {
            // Arrange
            var model = new GetAllActivesMotorcyclesQuery { SortBy = sortBy };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(query => query.SortBy);
        }

        [Theory]
        [InlineData(null)] // null should be valid as it is optional        
        public void SortBy_ValidWhenNull(string sortBy)
        {
            // Arrange
            var model = new GetAllActivesMotorcyclesQuery { SortBy = sortBy };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(query => query.SortBy);
        }

        [Theory]
        [InlineData("Color")]
        [InlineData("Mileage")]
        public void SortBy_Invalid(string sortBy)
        {
            // Arrange
            var model = new GetAllActivesMotorcyclesQuery { SortBy = sortBy };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(query => query.SortBy);
        }
    }
}