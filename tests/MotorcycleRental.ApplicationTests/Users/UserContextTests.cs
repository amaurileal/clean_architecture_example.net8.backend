﻿using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using MotorcycleRental.Domain.Constants;
using System.Security.Claims;
using Xunit;

namespace MotorcycleRental.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            // arrange
            var httpContextAcessorMock = new Mock<IHttpContextAccessor>();

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier,"1"),
                new(ClaimTypes.Email,"test@test.com"),
                new(ClaimTypes.Role,UserRoles.Admin),
                new(ClaimTypes.Role,UserRoles.Biker)
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            httpContextAcessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContext(httpContextAcessorMock.Object);

            // act
            var currentUser = userContext.GetCurrentUser();

            // assert
            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.Biker);

        }

        [Fact()]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            // arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

            var userContext = new UserContext(httpContextAccessorMock.Object);

            // act

            Action action = () => userContext.GetCurrentUser();

            // assert

            action.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("User context is not present");
        }

        
    }
}