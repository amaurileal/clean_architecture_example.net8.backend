using Amazon.S3;
using Amazon.S3.Model;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.Users;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;

namespace MotorcycleRental.Application.Bikers.Commands.SendCHNImage
{
    public class SendCNHImageCommandHandler(
        IAmazonS3 s3Client,
        IBikersRepository bikersRepository,
        ILogger<SendCNHImageCommandHandler> logger,
        IUserContext userContext,
        IConfiguration config
        ) : IRequestHandler<SendCNHImageCommand, bool>
    {
        public async Task<bool> Handle(SendCNHImageCommand request, CancellationToken cancellationToken)
        {
            //Verify if the userContext is a Biker
            var currentUser = userContext.GetCurrentUser();

            var biker = await bikersRepository.GetByUserIdAsync(currentUser.Id);
            if (biker == null)
                throw new BadRequestException($"Biker not found for email: {currentUser.Email}");


            if (request.CNHImage == null || request.CNHImage.ContentType != "image/png" && request.CNHImage.ContentType != "image/bmp")
            {
                throw new BadRequestException("Invalid file format. Only PNG and BMP files are allowed.");
            }
            var key = $"{Path.GetFileNameWithoutExtension(request.CNHImage.FileName)}-{Guid.NewGuid()}{Path.GetExtension(request.CNHImage.FileName)}";

            using (var stream = new MemoryStream())
            {
                await request.CNHImage.CopyToAsync(stream);
                stream.Position = 0;
                var requestAWS = new PutObjectRequest
                {
                    BucketName = config["S3Bucket"],
                    Key = key,
                    InputStream = stream,
                    ContentType = request.CNHImage.ContentType
                };
                await s3Client.PutObjectAsync(requestAWS);
            }

            //insert image reference to Biker
            biker.CHNImg = key;

            await bikersRepository.SaveChanges();

            return true;

        }
    }
}
