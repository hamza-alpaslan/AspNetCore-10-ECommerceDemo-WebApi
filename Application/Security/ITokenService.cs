using Domain.Entities;

namespace Web.Security
{
    public interface ITokenService
    {
        public JwtSettings JwtSettings { get; set; }
        public Task<string> GenerateToken(ApplicationUser applicationUser);
        public Task<string> GenerateRefreshToken();
    }
}
