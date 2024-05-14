using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Application.Bikers.Dtos;
using MotorcycleRental.Application.Users;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;

namespace MotorcycleRental.Application.Bikers.Queries.GetRegister
{
    public class GetRegisterQueryHandler(
        IAmazonS3 s3Client,
        IBikersRepository bikersRepository,
        ILogger<GetRegisterQueryHandler> logger,
        IUserContext userContext,
        IMapper mapper,
        IConfiguration config
        ) : IRequestHandler<GetRegisterQuery, BikerDto>
    {
        public async Task<BikerDto> Handle(GetRegisterQuery request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("Getting Biker Register From User: {AuthenticatedUser}",currentUser);

            var biker = await bikersRepository.GetByUserIdAsync(currentUser.Id);

            if (biker == null)
            throw new NotFoundException(nameof(biker),$"Biker for user Email: {currentUser.Email}");

            var bikerDto = mapper.Map<BikerDto>(biker);

            bikerDto.CNHUrl = getCHNImage(bikerDto.CHNImg);

            return bikerDto;
        }

        private string? getCHNImage(string key)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = config["S3Bucket"],
                Key = key,
                Expires = DateTime.UtcNow.AddMinutes(5)
            };

            var url = s3Client.GetPreSignedURL(request);

            return url;
        }
    }
}
