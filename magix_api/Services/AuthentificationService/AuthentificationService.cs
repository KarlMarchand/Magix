using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace magix_api.Services.AuthentificationService
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IConfiguration _config;

        public AuthentificationService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(Player player)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("id", player.Id.ToString()),
                new Claim("key", player.Key ?? string.Empty),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}