using Amazon.Runtime.Internal.Util;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.Bikers.Commands.SendCHNImage;
using MotorcycleRental.Application.Users;
using MotorcycleRental.Domain.Constants;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Repositories;
using System.Text;

namespace MotorcycleRental.ApplicationTests.Bikers.Commands.SendCNHImage.Commons;

public class SendCNHImageCommandTestSetup
{
    public Mock<IUserContext> UserContextMock { get; }
    public Mock<IBikersRepository> BikersRepositoryMock { get; }
    public Mock<IAmazonS3> S3ClientMock { get; }
    public Mock<Microsoft.Extensions.Configuration.IConfiguration> ConfigMock { get; }
    public Mock<IConfigurationSection> ConfigSectionMock { get; }
    public CurrentUser TestUser { get;  }

    public Mock<ILogger<SendCNHImageCommandHandler>> LoggerMock { get; }

    public SendCNHImageCommandTestSetup()
    {
        UserContextMock = new Mock<IUserContext>();
        BikersRepositoryMock = new Mock<IBikersRepository>();
        S3ClientMock = new Mock<IAmazonS3>();
        ConfigMock = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
        ConfigSectionMock = new Mock<IConfigurationSection>();
        LoggerMock = new Mock<ILogger<SendCNHImageCommandHandler>>();

        //TestUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin,UserRoles.Biker]);

        TestUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.Biker]);

        // Setup the configuration section mock
        ConfigSectionMock.SetupGet(x => x.Value).Returns("motorcycle-rent-temp");
        ConfigMock.Setup(x => x.GetSection("S3Bucket")).Returns(ConfigSectionMock.Object);

        UserContextMock.Setup(x => x.GetCurrentUser()).Returns(TestUser);
    }

    public SendCNHImageCommandHandler CreateHandler()
    {
        return new SendCNHImageCommandHandler(S3ClientMock.Object, BikersRepositoryMock.Object, LoggerMock.Object, UserContextMock.Object, ConfigMock.Object);
    }

    public FormFile CreateFormFile(string content, string fileName, string contentType)
    {
        // Garante que o conteúdo não seja nulo ou vazio
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentException("Content cannot be null or empty", nameof(content));
        }

        // Cria um MemoryStream baseado no conteúdo (em bytes)
        var contentBytes = Encoding.UTF8.GetBytes(content);
        var stream = new MemoryStream(contentBytes);

        // Garante que o stream esteja no início
        stream.Position = 0;

        // Cria o FormFile com todos os parâmetros necessários
        var formFile = new FormFile(stream,baseStreamOffset: 0, length: stream.Length, name: "file", fileName: fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };

        return formFile;
    }

}
