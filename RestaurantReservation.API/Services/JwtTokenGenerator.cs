using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.API.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantReservation.API.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiryInHours;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            _secretKey = jwtSettings.GetValue<string>("Key");
            _issuer = jwtSettings.GetValue<string>("Issuer");
            _audience = jwtSettings.GetValue<string>("Audience");
            _expiryInHours = jwtSettings.GetValue<int>("ExpiryInHours");
        }


        public string GenerateToken(string userName, string password)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_expiryInHours),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
