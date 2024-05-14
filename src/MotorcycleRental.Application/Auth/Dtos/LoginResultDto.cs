namespace MotorcycleRental.Application.Auth.Dtos
{
    public class LoginResultDto
    {
        public string TokenType { get; set; } = default!;

        public string AccessToken { get; set; } = default!;

        public int ExpiresIn { get; set; }

        public string RefreshToken { get; set; } = default!;
    }
}
