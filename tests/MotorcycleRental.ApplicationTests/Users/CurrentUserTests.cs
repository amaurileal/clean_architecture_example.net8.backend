using FluentAssertions;
using MotorcycleRental.Domain.Constants;
using Xunit;

namespace MotorcycleRental.Application.Users.Tests
{
    public class CurrentUserTests
    {
        [Fact()]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue()
        {
            // arrange
            
            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.Biker]);

            // act
            
            var isInRole = currentUser.IsInRole(UserRoles.Admin);

            // assert

            isInRole.Should().BeTrue();


        }

        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            // arrange

            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.Biker]);

            // act

            var isInRole = currentUser.IsInRole("User");

            // assert

            isInRole.Should().BeFalse();


        }

        [Fact()]
        public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
        {
            // arrange

            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.Biker]);

            // act

            var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

            // assert

            isInRole.Should().BeFalse();


        }

        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.Biker)]
        public void IsInRole_WithNoMatchingRoles_ShouldReturnTrue(string roleName)
        {
            // arrange

            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.Biker]);

            // act

            var isInRole = currentUser.IsInRole(roleName);

            // assert

            isInRole.Should().BeTrue();


        }
    }
}