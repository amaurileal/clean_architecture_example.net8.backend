using Amazon.S3;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.Application.Auth.Services;
using MotorcycleRental.Application.Users;

namespace MotorcycleRental.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {

            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
           
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
            services.AddAutoMapper(applicationAssembly);

            services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

            // Amazon S3 Configurations
            services.AddSingleton<IAmazonS3>(sp =>
            {
                var clientConfig = new AmazonS3Config
                {
                    RegionEndpoint = Amazon.RegionEndpoint.USEast1 // ou a região que você está utilizando
                };
                return new AmazonS3Client(clientConfig);
            });

            services.AddScoped<IUserContext,UserContext>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddHttpContextAccessor();
        }
    }
}
