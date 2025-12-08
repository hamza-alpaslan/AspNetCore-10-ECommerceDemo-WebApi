using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Web.Security
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        //TODO: Constructur da user manager ı ata
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;

        }
        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng =RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshToken=Convert.ToBase64String(randomNumber);
            return refreshToken;
        }

        public async Task<string> GenerateToken(ApplicationUser applicationUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claimArry = new[]
            {
                new Claim (ClaimTypes.NameIdentifier, applicationUser.Name),
                new Claim(ClaimTypes.Role, applicationUser.Role)
            };
            var token = new JwtSecurityToken(_jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimArry,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiresMinutes),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
