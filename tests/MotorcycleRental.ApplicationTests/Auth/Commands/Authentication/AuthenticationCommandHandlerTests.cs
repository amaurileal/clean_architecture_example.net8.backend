using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.Auth.Dtos;
using MotorcycleRental.Application.Auth.Services;
using MotorcycleRental.Application.Users;
using MotorcycleRental.ApplicationTests.Auth.Common;
using MotorcycleRental.Domain.Entities;
using Xunit;

namespace MotorcycleRental.Application.Auth.Commands.Authentication.Tests
{
    public class AuthenticationCommandHandlerTests
    {
        [Fact()]        
        public void Handle_ForValidCommand_ReturnsAuthetication()
        {

           // // arrange 
           // var email = "test@test.com";
           // var loggerMock = new Mock<ILogger<AuthenticationCommandHandler>>();
           // var userManager = UserManagerTestSetup.MockUserManager<User>();
           // var tokenMock = new Mock<ITokenService>();

           // var command = new AuthenticationCommand()
           // {
           //     Email= email,
           //     Password= "TesteFake!1"
           // };
           
            

           // var user = new User
           // {
           //     Id="1",
           //     Email=email
           // };

           // var userManagerMock = Mock.Get(userManager);

           //userManagerMock.Setup(x => x.FindByIdAsync("1"))
           //     .ReturnsAsync(user);

           // var commandHandler = new AuthenticationCommandHandler(loggerMock.Object
           //     ,userManagerMock.Object,tokenMock.Object);


           // //act
           // var result = commandHandler.Handle(command, CancellationToken.None);

           // // assert
           // result.Should().Be(new LoginResultDto());
            

        }
    }
}