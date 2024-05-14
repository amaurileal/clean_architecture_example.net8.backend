using Microsoft.Extensions.Configuration;
using MotorcycleRental.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace MotorcycleRental.Application.Auth.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);

        string GenerateRefreshToken(User user);

        TokenValidationResult validateRefreshToken(string token);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;

        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]!));
            _userManager = userManager;
        }

        public async Task<string> GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
                //new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {

                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //Adding Other Claims

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["TokenExpirationInMinutes"])),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        public string GenerateRefreshToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key =  Encoding.UTF8.GetBytes(_config["JWT:RefreshTokenKey"]!);            
            var claimList = new List<Claim>{
                    new Claim("email",user.Email)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["RefreshTokenExpirationInMinutes"]!)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public TokenValidationResult validateRefreshToken(string token)
        {
            var tokenHandler = new JsonWebTokenHandler();
            var secret = _config["JWT:RefreshTokenKey"];
            var key = Encoding.ASCII.GetBytes(secret);

            var result = tokenHandler.ValidateToken(token, new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            });

            return result;
        }
    }
}
