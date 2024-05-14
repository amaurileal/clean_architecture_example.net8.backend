using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace MotorcycleRental.ApplicationTests.Auth.Common
{
    public static class UserManagerTestSetup
    {
        public static UserManager<TUser> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var options = new Mock<IOptions<IdentityOptions>>();
            var hasher = new Mock<IPasswordHasher<TUser>>();
            var userValidators = new List<IUserValidator<TUser>>();
            var passwordValidators = new List<IPasswordValidator<TUser>>();
            var normalizer = new Mock<ILookupNormalizer>();
            var describer = new Mock<IdentityErrorDescriber>();
            var serviceProvider = new Mock<IServiceProvider>();
            var logger = new Mock<ILogger<UserManager<TUser>>>();

            return new UserManager<TUser>(
                store.Object, options.Object, hasher.Object, userValidators, passwordValidators,
                normalizer.Object, describer.Object, serviceProvider.Object, logger.Object);
        }
    }
}
